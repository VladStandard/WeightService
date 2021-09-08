// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник XAML.
    /// </summary>
    public sealed class XamlHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<XamlHelper> _instance = new Lazy<XamlHelper>(() => new XamlHelper());
        public static XamlHelper Instance => _instance.Value;

        private XamlHelper()
        {
            //
        }

        #endregion

        /// <summary>
        /// Элемент как шаблон.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public DataTemplate GetElementAsTemplate(FrameworkElement element)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(
                "<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">{0}</DataTemplate>",
                System.Windows.Markup.XamlWriter.Save(element));

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(builder.ToString());
                    writer.Flush();

                    stream.Position = 0;
                    DataTemplate template = (DataTemplate)System.Windows.Markup.XamlReader.Load(stream);
                    return template;
                }
            }
        }

        /// <summary>
        /// Центрировать окно.
        /// </summary>
        /// <param name="window">Окно</param>
        public void CenterWindowOnScreen(Window window)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            window.Left = (screenWidth / 2) - (window.Width / 2);
            window.Top = (screenHeight / 2) - (window.Height / 2);
        }

        /// <summary>
        /// Центрировать окно.
        /// </summary>
        /// <param name="windowChild">Дочернее окно.</param>
        /// <param name="windowOwner">Родительское окно.</param>
        public void CenterWindowOnWindow(Window windowChild, Window windowOwner)
        {
            windowChild.Left = windowOwner.Left + (windowOwner.Width / 2) - windowChild.Width / 2;
            windowChild.Top = windowOwner.Top + (windowOwner.Height / 2) - windowChild.Height / 2;
        }

        /// <summary>
        /// Копия холста.
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public Canvas GetCanvasCopy(Canvas canvas)
        {
            Canvas result = new Canvas
            {
                Width = canvas.ActualWidth,
                Height = canvas.ActualHeight,
                RenderTransform = new ScaleTransform(0.90, 0.95),
            };
            foreach (UIElement child in canvas.Children)
            {
                string xaml = System.Windows.Markup.XamlWriter.Save(child);
                if (System.Windows.Markup.XamlReader.Parse(xaml) is UIElement deepCopy)
                    result.Children.Add(deepCopy);
            }
            return result;
        }

        /// <summary>
        /// Печать.
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="description"></param>
        public void Print(Canvas canvas, string description)
        {
            if (canvas == null)
            {
                MessageBox.Show("Не заполнена область печати!");
                return;
            }
            if (canvas.Width < 100 || canvas.Height < 100)
            {
                MessageBox.Show("Область печати имеет недопустимый размер!");
                return;
            }

            //var children = canvas.Children.Cast<UIElement>().ToArray();
            Canvas canvasPrint = GetCanvasCopy(canvas);

            // Получить PrintTicket по умолчанию с принтера.
            System.Printing.LocalPrintServer localPrintServer = new System.Printing.LocalPrintServer();
            // Получение коллекции локального принтера на компьютере пользователя.
            System.Printing.PrintQueueCollection localPrinterCollection = localPrintServer.GetPrintQueues();
            System.Collections.IEnumerator localPrinterEnumerator = localPrinterCollection.GetEnumerator();
            if (localPrinterEnumerator.MoveNext())
            {
                // Получить PrintQueue с первого доступного принтера.
                System.Printing.PrintQueue printQueue = (System.Printing.PrintQueue)localPrinterEnumerator.Current;
                // Получить PrintTicket по умолчанию с принтера.
                if (printQueue != null)
                {
                    System.Printing.PrintTicket printTicket = printQueue.DefaultPrintTicket;
                    System.Printing.PageMediaSize pageMediaSize = new System.Printing.PageMediaSize(canvasPrint.Width, canvasPrint.Height);
                    printTicket.PageMediaSize = pageMediaSize;
                    //printTicket.PageMediaType = System.Printing.PageMediaType.Unknown;
                    //var printCapabilites = printQueue.GetPrintCapabilities();
                    // Modify PrintTicket
                    //if (printCapabilites.CollationCapability.Contains(System.Printing.Collation.Collated))
                    //    printTicket.Collation = System.Printing.Collation.Collated;
                    //if (printCapabilites.DuplexingCapability.Contains(System.Printing.Duplexing.TwoSidedLongEdge))
                    //    printTicket.Duplexing = System.Printing.Duplexing.TwoSidedLongEdge;
                    //if (printCapabilites.StaplingCapability.Contains(System.Printing.Stapling.StapleDualLeft))
                    //    printTicket.Stapling = System.Printing.Stapling.StapleDualLeft;
                    PrintDialog printDialog = new PrintDialog
                    {
                        //PrintQueue = printQueue,
                        PrintTicket = printTicket,
                    };
                    printDialog.PrintQueue.Commit();
                    if (printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintVisual(canvasPrint, description);
                    }
                }
            }
        }
    }
}
