using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFCanvas_Move_Drog.ViewModels
{
    public class PointItemViewModel
    {
        /// <summary>
        /// Canvas 畫布本身是移動不了，在 Canvas 畫布裡面是可以移動的，所以 Canvas 嵌套一個 Canvas，給 Canvas 做一個背景圖片，這張圖片就可以隨意移動了，
        /// 圖片裡面的圖標也要移動，那就是圖標放存入到 Canvas 這張背景圖中，圖標是傳過來的數量是 N，
        /// 因為不確定數量只能寫在後台產生。
        /// </summary>
        public PointItemViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                PointItem obj = new PointItem()
                {
                    X = i * 10 + 50D,
                    Y = i * 10 + 100D,
                    Width = 20,
                    Height = 20,
                    Tag = i.ToString(),
                    ToolTip = i.ToString(),
                    Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Images/point.png")))
                };

                obj.DragDelta += Thumb_DragDelta;
                obj.DragCompleted += Thumb_DragCompleted;

                PointItems.Add(obj);
            }
        }


        #region -- Field & Properties ---

        public Dictionary<string, double[]> dict = new Dictionary<string, double[]>();

        private ObservableCollection<PointItem> _pointItems = new ObservableCollection<PointItem>();
        public ObservableCollection<PointItem> PointItems
        {
            get { return _pointItems; }
            set { _pointItems = value; }
        }

        #endregion


        #region -- Private Methods --

        /// <summary>
        /// 移動後的座標
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            // throw new NotImplementedException();

            PointItem tbSender = (PointItem)sender;

            string ys = tbSender.Tag.ToString();
            double[] arr = new double[2];

            arr[0] = tbSender.X;
            arr[1] = tbSender.Y;

            if (dict.Where(x => x.Key == ys).ToList().Count > 0)
            {
                dict.Remove(ys);
                dict.Add(ys, arr);
            }
            else
            {
                dict.Add(ys, arr);
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            PointItem tbSender = (PointItem)sender;

            tbSender.X += e.HorizontalChange;
            tbSender.Y += e.VerticalChange;

            Canvas.SetLeft(tbSender, tbSender.X);
            Canvas.SetTop(tbSender, tbSender.Y);
        }

        #endregion
    }
}
