using LiveChartsCore.SkiaSharpView;
using System.Text;
using static unvell.Common.Win32Lib.Win32;

namespace MainUI.Procedure.Curve
{
    public partial class frmCurve : UIForm
    {
        private readonly string folderPath = Application.StartupPath + "Autosave\\";
        public frmCurve()
        {
            InitializeComponent();
            InitViewRecovery();
            InitializeParameterNames();
            InitializeChart();
            uiTimeStart.Value = DateTime.Now;
        }

        /// <summary>
        /// 获取CSV数据构建临时表单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public Dictionary<string, DataTable> GetDataTable(DateTime startTime)
        {
            Dictionary<string, DataTable> dts = [];
            string path = folderPath + startTime.ToString("yyyy-MM-dd");
            if (!Path.Exists(path)) return dts;
            // 方法来注册自定义编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FileInfo[] files = SearchFilesByDate(path);
            foreach (FileInfo file in files)
            {
                dts.Add(file.ToString(), CSVHelper.ReadAsDatatable(file.ToString()));
            }
            return dts;
        }

        /// <summary>
        /// 扫描全部CSV文件
        /// </summary>
        /// <param name="folderPath">文件存放路径</param>
        /// <returns></returns>
        public static FileInfo[] SearchFilesByDate(string folderPath)
        {
            DirectoryInfo dirInfo = new(folderPath);
            return dirInfo.GetFiles();
        }

        /// <summary>
        /// 加载处理后数据
        /// </summary>
        private void LoadCsvData()
        {
            try
            {
                Debug.WriteLine("数据处理开始时间：" + DateTime.Now);
                var timeStart = uiTimeStart.Value;
                var datas = GetDataTable(timeStart);
                if (datas.Count == 0) { MessageBox.Show(this, $"未找到[{timeStart:yyyy-MM-dd}]的数据！"); return; }
                frmCurveDatas frmCurveData = new(datas, uiComBoxModel.Text);
                frmCurveData.ShowDialog();
                if (frmCurveData.DialogResult != DialogResult.OK)
                    return;
                TaskStart(datas[frmCurveData.Dickey]);
                Debug.WriteLine("数据处理结束时间：" + DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "加载数据失败！" + ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LoadCsvData();
            InitLineSeries();
            InitializeChart();
        }

        CancellationTokenSource tokenSource = new();
        /// <summary>
        /// 启动曲线控件
        /// </summary>
        /// <returns></returns>
        private async void TaskStart(DataTable data)
        {
            tokenSource = new CancellationTokenSource();
            await Task.Run(() =>
              {
                  try
                  {
                      for (int i = 0; i < data.Rows.Count && !tokenSource.IsCancellationRequested; i++)
                      {
                          var time = data.Rows[i]["时间"].ToDateTime();
                          double pressure = data.Rows[i]["压力"].ToDouble();
                          double discharge = data.Rows[i]["流量"].ToDouble();

                          for (int ii = 0; ii < _allSeries.Count; ii++)
                          {
                              var lineSeries = (LineSeries<DateTimePoint>)_allSeries[ii];
                              var values = (ObservableCollection<DateTimePoint>)lineSeries.Values;
                              switch (ii)
                              {
                                  case 0:
                                      values.Add(new DateTimePoint(time, pressure));
                                      break;
                                  case 1:
                                      values.Add(new DateTimePoint(time, discharge));
                                      break;
                                  default:
                                      break;
                              }
                          }
                          Task.Delay(uiUpDownRate.Value).Wait();
                      }
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(this, ex.Message, "系统提示");
                  }
              }, tokenSource.Token);

            MessageBox.Show(this, "曲线回放完成！", "系统提示");
        }

        #region 曲线控件
        // 曲线名称列表
        private string[] _parameterNames;
        // 用于存储所有系列数据
        private ObservableCollection<ISeries> _allSeries;
        // 用于图表实际显示的系列数据
        private ObservableCollection<ISeries> _visibleSeries;

        // 初始化曲线名称
        private void InitializeParameterNames()
        {
            _parameterNames =
            [
                "喷淋压力(Kpa)",
                "喷淋流量(m³/h)",
            ];
        }

        // 初始化折线
        private void InitLineSeries()
        {
            _allSeries = [];
            _visibleSeries = [];
            for (int i = 0; i < _parameterNames.Length; i++)
            {
                string paramName = _parameterNames[i].ToLower();
                var points = new ObservableCollection<DateTimePoint>();
                LineSeries<DateTimePoint> lineSeries = new()
                {
                    Fill = null,
                    Values = points, //绑定值
                    LineSmoothness = 0.1f,// 设置线条平滑度
                    Name = _parameterNames[i],
                    Stroke = new SolidColorPaint(GetParameterSKColor(i), 1.5f),//线条宽度
                    GeometrySize = 2,//外点大小
                    GeometryFill = new SolidColorPaint(GetParameterSKColor(i), 2),//内点颜色
                    GeometryStroke = new SolidColorPaint(GetParameterSKColor(i), 2), //内点大小
                    ScalesYAt = paramName.Contains("流量") ? 1 : 0
                };
                _allSeries.Add(lineSeries);
                _visibleSeries.Add(lineSeries);
            }
        }

        // 初始化视图还原按钮
        private void InitViewRecovery()
        {
            try
            {
                Label zoomInstructionLabel = new()
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(64, 64, 64),
                    Text = "提示：鼠标滚轮可放大缩小，按住鼠标左键拖动可平移图表，按住鼠标右键平移可以局部放大",
                };
                Color color = Color.FromArgb(90, 124, 236);
                UIButton zoomButton = new()
                {
                    Text = "还原视图",
                    Dock = DockStyle.Right,
                    Size = new Size(120, 30),
                    ForeColor = Color.FromArgb(225, 255, 255),
                    Font = new Font("思源黑体 CN Bold", 13),
                    BackColor = Color.FromArgb(255, 255, 255),
                    FillColor = color,
                    FillColor2 = color,
                    RectColor = color,
                    RectDisableColor = color,
                };
                zoomButton.Click += (s, e) =>
                {
                    // 还原视图
                    if (LineCurve.XAxes != null && LineCurve.XAxes.Any())
                    {
                        foreach (var axis in LineCurve.XAxes)
                        {
                            axis.MinLimit = null;
                            axis.MaxLimit = null;
                        }
                    }

                    if (LineCurve.YAxes != null && LineCurve.YAxes.Any())
                    {
                        foreach (var axis in LineCurve.YAxes)
                        {
                            axis.MinLimit = null;
                            axis.MaxLimit = null;
                        }
                    }
                    LineCurve.Invalidate();
                };
                Panel panel = new()
                {
                    Dock = DockStyle.Bottom,
                    Height = 30,
                    BackColor = Color.FromArgb(255, 255, 255),
                };
                panel.Controls.Add(zoomButton);
                panel.Controls.Add(zoomInstructionLabel);
                LineCurve.Controls.Add(panel);
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"加载还原视图按钮错误：{ex.Message}");
            }
        }

        // 初始化图表
        private void InitializeChart()
        {
            LiveCharts.Configure(config =>
            config
                .HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉'))
             //.UseRightToLeftSettings() // 启用从右到左的工具提示      
             );

            // 缩放模式
            LineCurve.ZoomMode = ZoomAndPanMode.Both;
            if (LineCurve.XAxes != null && LineCurve.XAxes.Any())
            {
                foreach (var axis in LineCurve.XAxes)
                {
                    axis.MinLimit = null; // 启用X轴缩放
                    axis.MaxLimit = null;
                    axis.MinZoomDelta = 0.1;  // 控制最小缩放增量
                }
            }
            if (LineCurve.YAxes != null && LineCurve.YAxes.Any())
            {
                foreach (var axis in LineCurve.YAxes)
                {
                    axis.MinLimit = null; // 启用Y轴缩放
                    axis.MaxLimit = null;
                    axis.MinZoomDelta = 0.1;  // 控制最小缩放增量
                }
            }

            // 设置图表
            LineCurve.Series = _visibleSeries;
            LineCurve.MouseWheel += (sender, e) =>
            {
                // 如果按住Ctrl键，则仅缩放X轴
                if (ModifierKeys == Keys.Control)
                {
                    // 手动计算X轴缩放
                    var deltaZoom = e.Delta > 0 ? 0.2 : -0.2;
                    foreach (var axis in LineCurve.XAxes)
                    {
                        if (axis.MinLimit != null && axis.MaxLimit != null)
                        {
                            var range = axis.MaxLimit.Value - axis.MinLimit.Value;
                            var center = (axis.MaxLimit.Value + axis.MinLimit.Value) / 2;
                            var newRange = range * (1 - deltaZoom);
                            axis.MinLimit = center - newRange / 2;
                            axis.MaxLimit = center + newRange / 2;
                        }
                    }
                    LineCurve.Invalidate();
                }
            };
            // 设置图表标题和轴标签
            LineCurve.Title = new LabelVisual
            {
                Text = "淋雨试验实时曲线",
                TextSize = 22,
                Paint = new SolidColorPaint(SKColors.Black)
            };

            // 设置Y轴
            LineCurve.YAxes =
            [
                new Axis
                {
                    MinLimit = 0, // 设置Y轴最小值
                    MaxLimit = 1000, // 设置Y轴最大值
                    Name = "喷淋压力(Kpa)",
                    LabelsDensity = 0.85f, //轴密度，默认值为 0.85，小于 0 的值将使标签重叠。
                    InLineNamePlacement = false, // 名称顶部(会导致顶部图例存在位置与顶部曲线重叠)
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                },
                new Axis
                {

                    MinLimit = 0, // 设置Y轴最小值
                    MaxLimit = 100, // 设置Y轴最大值
                    Name = "喷淋流量(m³/h)",
                    LabelsDensity = 0.85f, //轴密度，默认值为 0.85，小于 0 的值将使标签重叠。
                    Position = AxisPosition.End,
                    InLineNamePlacement = false, // 名称顶部(会导致顶部图例存在位置与顶部曲线重叠)
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                }
            ];
            // 设置X轴
            LineCurve.XAxes =
            [
                new Axis
                {
                     Labeler = value =>
                     {
                         // 检查值是否在有效的DateTime范围内
                         if (value >= DateTime.MinValue.Ticks && value <= DateTime.MaxValue.Ticks)
                         {
                             return new DateTime((long)value).ToString("HH:mm:ss");
                         }
                         else
                         {
                             return "--:--:--"; // 显示无效时间的占位符
                         }
                     },
                    Name = "时间(S)",
                    LabelsRotation = 0, // 标签旋转角度
                    //设置X轴的最小值为当前时间减去60秒的Ticks，最大值为当前时间加上60秒的Ticks
                    //MinLimit = DateTime.Now.AddSeconds(-10).Ticks,
                    //MaxLimit = DateTime.Now.AddSeconds(20).Ticks,
                    MinStep = TimeSpan.FromSeconds(1).Ticks, // 使用采样周期作为最小步长
                    UnitWidth = TimeSpan.FromSeconds(1).Ticks, // 使用采样周期作为单位宽度
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    TextSize = 16, // 设置文本大小
                    LabelsAlignment = Align.End, // 设置标签的对齐方式
                    Position = AxisPosition.Start,  // 设置轴的位置（底部）
                    ShowSeparatorLines = true, // 显示分隔线
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 1, // 分隔线的粗细
                        PathEffect = new DashEffect([3, 3]) // 分隔线的虚线效果
                    },
                }
            ];

            LineCurve.TooltipTextPaint = new SolidColorPaint(SKColors.Black);
            LineCurve.LegendTextPaint = new SolidColorPaint(SKColors.Black);// 设置图例
            LineCurve.LegendPosition = LegendPosition.Top;
            LineCurve.LegendTextSize = 16;
        }

        // 初始化折线颜色
        private SKColor GetParameterSKColor(int index)
        {
            // 使用HSV颜色空间生成均匀分布的鲜艳颜色
            // 黄金角度约为137.5°，用于在色轮上均匀分布颜色
            double goldenAngleRatio = 0.618033988749895;

            // 使用索引乘以黄金角度比例，确保颜色均匀分布
            double hue = (index * goldenAngleRatio * 360) % 360;

            // 降低亮度，增加饱和度，使颜色更深更鲜艳
            double saturation = 1.0;  // 100%饱和度
            double brightness = 0.6;  // 60%亮度 - 比之前的95%亮度更深

            // 使用参数索引来稍微变化亮度，确保不同的深色
            if (index % 3 == 0)
            {
                brightness = 0.65;  // 65%亮度
            }
            else if (index % 3 == 1)
            {
                brightness = 0.75;  // 55%亮度
            }
            else
            {
                brightness = 0.60;  // 60%亮度
            }

            // 使用HSL转换为SKColor
            return SKColor.FromHsl(
                (float)hue,
                (float)(saturation * 100),
                (float)(brightness * 100)
            );
        }
        #endregion

        private void btnStop_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
