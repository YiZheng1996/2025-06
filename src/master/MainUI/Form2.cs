using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WinForms;
using MainUI.CurrencyHelper;
using NLog.Layouts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;

namespace MainUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            InitializeParameterNames();
            InitializeChart();
            _updateTimer = new System.Timers.Timer(1000);
            _updateTimer.Elapsed += (s, e) =>
            {
                // 更新UI时需要调用UI线程
                BeginInvoke(new Action(() =>
                {
                    UpdateTimer_Tick(s, e);
                }));
            };
            _updateTimer.AutoReset = true;
        }

        // 曲线名称列表
        private string[] _parameterNames;
        // 定时器用于更新数据
        private System.Timers.Timer _updateTimer;
        // 用于存储所有系列数据
        private ObservableCollection<ISeries> _allSeries;
        // 用于图表实际显示的系列数据
        private ObservableCollection<ISeries> _visibleSeries;
        public ObservableCollection<ObservableCollection<DateTimePoint>> _dateTimePoints;

        private void InitializeParameterNames()
        {
            _parameterNames =
            [
                "气压传感器",
                "温度传感器",
            ];
        }

        private void InitializeChart()
        {
            LiveCharts.Configure(config =>
            config
                .HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉'))
                .UseRightToLeftSettings() // 启用从右到左的工具提示      
             );
            _allSeries = [];
            _visibleSeries = [];
            _dateTimePoints = [];

            for (int i = 0; i < _parameterNames.Length; i++)
            {
                string paramName = _parameterNames[i].ToLower();
                var points = new ObservableCollection<DateTimePoint>
                {
                    new(DateTime.Now, 0)
                };
                _dateTimePoints.Add(points);
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
                    ScalesYAt = paramName.Contains("温度") ? 1 : 0
                };
                _allSeries.Add(lineSeries);
                _visibleSeries.Add(lineSeries);
            }

            // 缩放模式
            cartesianChart1.ZoomMode = ZoomAndPanMode.Both;
            if (cartesianChart1.XAxes != null && cartesianChart1.XAxes.Any())
            {
                foreach (var axis in cartesianChart1.XAxes)
                {
                    axis.MinLimit = null; // 启用X轴缩放
                    axis.MaxLimit = null;
                    axis.MinZoomDelta = 0.1;  // 控制最小缩放增量
                }
            }
            if (cartesianChart1.YAxes != null && cartesianChart1.YAxes.Any())
            {
                foreach (var axis in cartesianChart1.YAxes)
                {
                    axis.MinLimit = null; // 启用Y轴缩放
                    axis.MaxLimit = null;
                    axis.MinZoomDelta = 0.1;  // 控制最小缩放增量
                }
            }

            Label zoomInstructionLabel = new()
            {
                Text = "提示：鼠标滚轮可放大缩小，按住鼠标左键拖动可平移图表，按住鼠标右键平移可以局部放大",
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft YaHei", 12),
                //Size = new Size(80, 30),
                AutoSize = true
            };
            UIButton zoomButton = new()
            {
                Text = "还原视图",
                Dock = DockStyle.Right,
                //Size = new Size(100, 30),
                Font = new Font("Microsoft YaHei", 12),
                ForeColor = Color.FromArgb(235, 227, 221),
                BackColor = Color.FromArgb(70, 75, 85),
            };

            Panel panel = new()
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.FromArgb(70, 75, 85),
            };
            panel.Controls.Add(zoomButton);
            panel.Controls.Add(zoomInstructionLabel);
            cartesianChart1.Controls.Add(panel);

            // 设置图表
            cartesianChart1.Series = _visibleSeries;
            cartesianChart1.MouseWheel += (sender, e) =>
            {
                // 如果按住Ctrl键，则仅缩放X轴
                if (ModifierKeys == Keys.Control)
                {
                    // 手动计算X轴缩放
                    var deltaZoom = e.Delta > 0 ? 0.2 : -0.2;
                    foreach (var axis in cartesianChart1.XAxes)
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
                    cartesianChart1.Invalidate();
                }
            };
            // 设置图表标题和轴标签
            cartesianChart1.Title = new LabelVisual
            {
                Text = "曲线案例",
                TextSize = 20,
                Paint = new SolidColorPaint(SKColors.Black)
            };

            // 设置Y轴
            cartesianChart1.YAxes =
            [
                new Axis
                {
                    MinLimit = 0, // 设置Y轴最小值
                    MaxLimit = 1000, // 设置Y轴最大值
                    Name = "气压传感器",
                    LabelsDensity = 0.85f, //轴密度，默认值为 0.85，小于 0 的值将使标签重叠。
                    InLineNamePlacement = false, // 名称顶部(会导致顶部图例存在位置与顶部曲线重叠)
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                },
                new Axis
                {
                    MinLimit = 0, // 设置Y轴最小值
                    MaxLimit = 500, // 设置Y轴最大值
                    Name = "温度传感器",
                    Position = AxisPosition.End,
                    InLineNamePlacement = false, // 名称顶部(会导致顶部图例存在位置与顶部曲线重叠)
                    NamePaint = new SolidColorPaint(SKColors.Black),
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                }
            ];
            // 设置X轴
            cartesianChart1.XAxes =
            [
                new Axis
                {
                    //设置X轴的最小值为当前时间减去60秒的Ticks，最大值为当前时间加上60秒的Ticks
                    MinLimit = DateTime.Now.AddSeconds(-10).Ticks,
                    //MaxLimit = DateTime.Now.AddSeconds(60).Ticks,
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
            cartesianChart1.TooltipTextPaint = new SolidColorPaint(SKColors.Black);
            cartesianChart1.LegendTextPaint = new SolidColorPaint(SKColors.Black);// 设置图例
            cartesianChart1.LegendPosition = LegendPosition.Top;
            cartesianChart1.LegendTextSize = 16;
        }

        private double GetRealParameterValue(int parameterIndex)
        {
            string paramName = _parameterNames[parameterIndex].ToLower();
            try
            {
                // 首先检查AIGrp中是否有对应的数据
                if (OPCHelper.AIgrp != null)
                {
                    if (paramName.Contains("传感器01"))
                        return OPCHelper.AIgrp[0];
                    else if (paramName.Contains("温度传感器"))
                        return OPCHelper.AIgrp[1];
                }
                return GetParameterValue(parameterIndex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"获取参数 {paramName} 的值时出错: {ex.Message}");
                // 发生异常时返回随机数据作为备用
                return GetParameterValue(parameterIndex);
            }
        }

        private double GetParameterValue(int parameterIndex)
        {
            // 这里替换为你的实际数据获取逻辑
            Random random = new();
            // 根据参数类型生成不同范围的随机值
            string paramName = _parameterNames[parameterIndex].ToLower();

            if (paramName.Contains("温度"))
            {
                return Math.Round(random.Next(20, 500) + random.NextDouble(), 1);
            }
            else if (paramName.Contains("湿度"))
            {
                return Math.Round(random.Next(20, 90) + random.NextDouble(), 1);
            }

            // 其他参数
            return Math.Round(random.Next(0, 1000) + random.NextDouble(), 1);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            // 更新所有系列的数据，不管是否可见
            for (int i = 0; i < _allSeries.Count; i++)
            {
                var lineSeries = (LineSeries<DateTimePoint>)_allSeries[i];
                var values = (ObservableCollection<DateTimePoint>)lineSeries.Values;
                var points = _dateTimePoints[i];
                // 获取对应参数的实际数据值
                double newValue = GetRealParameterValue(i);
                points.Add(new DateTimePoint(currentTime, newValue));
                // 添加新数据点
                //values.Add(newValue);
            }
        }

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
                brightness = 0.55;  // 55%亮度
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            cartesianChart1.XAxes.ToList()[0].MinLimit = DateTime.Now.AddSeconds(5).Ticks;
            _updateTimer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _updateTimer.Stop();
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Image files (*.png;*.jpg)|*.png;*.jpg",
                Title = "Save an Image File",
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                cartesianChart1.SaveToImage(path, ImageFormat.Png);
            }
        }

        private void btnResetView_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        // 重置视图
        private void ResetView()
        {
            if (cartesianChart1.XAxes != null && cartesianChart1.XAxes.Any())
            {
                foreach (var axis in cartesianChart1.XAxes)
                {
                    axis.MinLimit = null;
                    axis.MaxLimit = null;
                }
            }

            if (cartesianChart1.YAxes != null && cartesianChart1.YAxes.Any())
            {
                foreach (var axis in cartesianChart1.YAxes)
                {
                    axis.MinLimit = null;
                    axis.MaxLimit = null;
                }
            }
            cartesianChart1.Invalidate();
        }
    }
}
