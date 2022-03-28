using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFCanvas_Move_Drog.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// 實現一張平面圖可以放大、縮小不受邊框限制，鼠標左鍵按住可以隨意拖動，平面圖嵌入 N 個圖標或者點，這些圖標也可以隨意移動，
        /// 平面圖移動、圖標也跟著移動，座標保持不變。
        /// </summary>
        public MainViewModel()
        {
            DouWidth = 1550;
            DouHeight = 890;
            OriginDouScale = 1;
            SliderValue = OriginDouScale;
        }

        #region  -- Filed & Properties --

        private bool isMove = false;
        private Point oldPoint = new Point();

        public FrameworkElement CurrentMap { get; set; }

        /// <summary>
        /// 地圖寬度
        /// </summary>
        private double _douWidth;
        public double DouWidth
        {
            get { return _douWidth; }
            set { _douWidth = value; }
        }

        /// <summary>
        /// 地圖高度
        /// </summary>
        private double _douHeight;
        public double DouHeight
        {
            get { return _douHeight; }
            set { _douHeight = value; }
        }

        /// <summary>
        /// 地圖原始縮放尺寸
        /// </summary>
        public double OriginDouScale { get; set; }

        /// <summary>
        /// 地圖縮放後尺寸
        /// </summary>
        private double _douScale;
        public double DouScale
        {
            get { return _douScale; }
            set { _douScale = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 地圖 X 軸中心
        /// </summary>
        public double DouCenterX
        {
            get { return DouWidth / 2; }
        }

        /// <summary>
        /// 地圖 X 軸中心
        /// </summary>
        public double DouCenterY
        {
            get { return DouHeight / 2; }
        }


        private double _sliderValue;
        public double SliderValue
        {
            get { return _sliderValue; }
            set
            {
                DouScale = OriginDouScale + value / 100;
                _sliderValue = value;
            }
        }

        #endregion


        #region -- Private Methods --

        /// <summary>
        /// 鼠標左鍵按下
        /// </summary>
        public ICommand MapMoveStartCommand => new RelayCommand<MouseButtonEventArgs>(Canvas_MouseLeftButtonDown);
        private void Canvas_MouseLeftButtonDown(MouseButtonEventArgs e)
        {
            isMove = true;
            //Mouse.Capture(e.OriginalSource);
            Mouse.Capture((IInputElement)e.OriginalSource);
            oldPoint = e.GetPosition(null);
        }

        /// <summary>
        /// 鼠標左鍵放開
        /// </summary>
        public ICommand MapMoveEndCommand => new RelayCommand<MouseButtonEventArgs>(Canvas_MouseLeftButtonUp);
        private void Canvas_MouseLeftButtonUp(MouseButtonEventArgs e)
        {
            isMove = false;
            Mouse.Capture(null);
        }

        /// <summary>
        /// 產生圖標放入畫布背景圖中，圖標可以移動，記錄圖標座標，等保存後圖標直接產生所在的座標點
        /// </summary>
        public ICommand MapMovingCommand => new RelayCommand<MouseEventArgs>(Canvas_MouseMove);
        private void Canvas_MouseMove(MouseEventArgs e)
        {
            if (isMove)
            {
                FrameworkElement currEle = e.OriginalSource as FrameworkElement;

                double xPos = e.GetPosition(null).X - oldPoint.X + (double)currEle.GetValue(Canvas.LeftProperty);
                double yPos = e.GetPosition(null).Y - oldPoint.Y + (double)currEle.GetValue(Canvas.TopProperty);

                currEle.SetValue(Canvas.LeftProperty, xPos);
                currEle.SetValue(Canvas.TopProperty, yPos);

                oldPoint = e.GetPosition(null);
            }
        }

        //public ICommand HandleDropCommand => new RelayCommand<DragEventArgs>(MessageBoxShow);
        //private void Canvas_Drog(DragEventArgs e)
        //{

        //}

        private RelayCommand<DragEventArgs> _dropCommand;
        public RelayCommand<DragEventArgs> HandleDropCommand
        {
            get
            {
                return _dropCommand ?? (_dropCommand = new RelayCommand<DragEventArgs>(Drop));
            }
        }
        private static void Drop(DragEventArgs e)
        {
            // do something here
        }



        //public RelayCommand<DragEventArgs> DragCommand { get; private set; }

        //public void MessageBoxShow(DragEventArgs e)
        //{
        //    //MessageBox.Show("Drop");
        //    if (e.Data == null)
        //    {
        //        return;
        //    }

        //    var files = e.Data.GetData(DataFormats.FileDrop)
        //        as FileInfo[];

        //    // This works with multiple files, but in that
        //    // simple case, let's just handle the 1st one
        //    if (files == null
        //        || files.Length == 0)
        //    {
        //        //DroppedFileContent = "No files";
        //        return;
        //    }

        //    var file = files[0];

        //    if (!file.Extension.ToLower().Equals(".txt"))
        //    {
        //        //DroppedFileContent = "Not a TXT file";
        //        return;
        //    }

        //    using (var stream = file.OpenRead())
        //    {
        //        using (var reader = new StreamReader(stream))
        //        {
        //            // Read the first line
        //            var line = reader.ReadLine();
        //            //DroppedFileContent = line;
        //        }
        //    }
        //}


        #endregion
    }
}
