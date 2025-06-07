namespace MainUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitChart();
        }

        // 定义三个数据列表，用于存储不同类别的数据点
        List<DateTimePoint> Values1 { get; } = [];
        List<DateTimePoint> Values2 { get; } = [];
        List<DateTimePoint> Values3 { get; } = [];
        private void InitChart()
        {
            LiveCharts.Configure(config =>
            config
               .HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉'))
               //.UseRightToLeftSettings() // 启用从右到左的工具提示      
               );
            cartesianChart1.XAxes =
                [
                new Axis{
                        // 设置X轴的最小值为当前时间减去60秒的Ticks，最大值为当前时间加上60秒的Ticks
                        MinLimit = DateTime.Now.AddSeconds(-60).Ticks,
                        MaxLimit = DateTime.Now.AddSeconds(60).Ticks,

                        // 设置标签格式化器，以便每个刻度都显示为时间
                        Labeler = value => new DateTime((long)value).ToString("HH:mm:ss"),

                        // 设置单位宽度为1秒
                        UnitWidth = TimeSpan.FromSeconds(1).Ticks,

                        // 设置最小步长为1秒，确保每秒都有刻度
                        MinStep = TimeSpan.FromSeconds(1).Ticks,

                        // 显示分隔线
                        ShowSeparatorLines = true,
                        SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                        {
                            StrokeThickness = 1, // 分隔线的粗细
                            PathEffect = new DashEffect([3, 3]) // 分隔线的虚线效果
                        },

                        // 设置文本大小
                        TextSize = 16,
                        LabelsAlignment = Align.Start, // 设置标签的对齐方式
                        Position = AxisPosition.Start,  // 设置轴的位置（底部）
                }];

            cartesianChart1.YAxes =
                [
                  new Axis
                  {
                         DrawTicksPath = false, // 不显示刻度线
                         Name = "Y轴1", // 设置Y轴的名称
                         MinLimit = 0, // 设置最小值
                         MaxLimit = 600, // 设置最大值
                         ShowSeparatorLines = true, // 显示分隔线
                         SeparatorsPaint = new SolidColorPaint(new SKColor(0, 0, 0),1), // 分隔线的颜色和粗细
                         TextSize = 16, // 设置文本大小
                         LabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0)), // 设置标签颜色
                         InLineNamePlacement = true, // 名称顶部
                   },
                ];


            cartesianChart1.Series =
            [
                new LineSeries<DateTimePoint>
            {
                //DataLabelsSize = 20,
                //DataLabelsPaint = new SolidColorPaint(SKColors.Blue), //点显示值
                //DataLabelsPosition = DataLabelsPosition.Top,
                //DataLabelsFormatter = (point) => point.Coordinate.PrimaryValue.ToString("C2"), //显示单位
                LineSmoothness = 0,// 设置线条平滑度
                Fill = null,
                Values = Values1, // 设置数据点
                Name = "曲线1",  // 分类名称
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 1 }, //线条宽度
                GeometryFill = new SolidColorPaint(SKColors.DarkRed),//点颜色
                GeometrySize = 3, //点大小
                GeometryStroke = new SolidColorPaint(SKColors.DarkRed) { StrokeThickness = 3 } //点大小
            },
            new LineSeries<DateTimePoint>
            {
                Fill = null,
                Values = Values2,
                Name = "曲线2"  // 分类名称
            },
            new LineSeries<DateTimePoint>
            {
                Fill = null,
                Values = Values3,
                Name = "曲线3"  // 分类名称
            }
            ];

            //// 设置图例
            //cartesianChart1.LegendTextPaint = new SolidColorPaint
            //{
            //    Color = SKColors.Black,
            //};
            cartesianChart1.LegendPosition = LegendPosition.Right;
            cartesianChart1.LegendTextSize = 15;
            cartesianChart1.TooltipPosition = TooltipPosition.Bottom;
            cartesianChart1.ZoomMode = ZoomAndPanMode.ZoomX;
        }

        public LabelVisual Title { get; set; } =
        new LabelVisual
        {
            Text = "My chart title",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15)
        };

        private Random random = new();
        private void timer1_Tick(object sender, EventArgs e)
        {
            Values1.Add(new DateTimePoint(DateTime.Now, random.Next(1, 600)));
            Values2.Add(new DateTimePoint(DateTime.Now, random.Next(100, 600)));
            Values3.Add(new DateTimePoint(DateTime.Now, random.Next(50, 100)));
            if (Values1.Count > 60) //最多显示60个点
            {
                Values1.RemoveAt(0);
            }

            // 更新X轴的最小和最大限制
            cartesianChart1.XAxes.First().MinLimit = DateTime.Now.AddSeconds(-60).Ticks;
            cartesianChart1.XAxes.First().MaxLimit = DateTime.Now.AddSeconds(60).Ticks;
        }
    }
}
