using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SteamWorkshopLib
{
    
    public class WinResize
    {
        Window Wind
        {
            get; set;
        }

        UIElement Element;
        TimerTick timerTick = new TimerTick();
        API.Pos poscur;
        API.Pos last_poscur;
        double Width = 0;
        double Height = 0;
        private Size resizeSize;
        bool isMouseDoun = false;
        bool isMouseMoove = false;
        public WinResize(Window w)
        {
            Wind = w;
            Width = Wind.Width;
            Height = Wind.Height;
            timerTick.Tick += (sender, e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Released)
                    isMouseDoun = false;
                // if (isMouseDoun)
                Update();
                Wind.Width = Width < Wind.MinWidth ? Wind.MinWidth + 5 : Width;
                Wind.Height = Height < Wind.MinHeight ? Wind.MinHeight + 5 : Height;               
            };
            Wind.MouseLeftButtonDown += (sender, e) =>
            {
                if (e.LeftButton != MouseButtonState.Pressed)
                    return;
                if (isMouseMoove && isMouseDoun == false)
                    Wind.DragMove();
            };
            Wind.MouseEnter += (sender, e) => isMouseMoove = true;
            Wind.MouseLeave += (sender, e) => isMouseMoove = false;
            timerTick.Start();
        }
 
        public void RightDown(UIElement element)
        {
            Element = element;
            MouseHandlers(Element);
        }

        private void MouseHandlers(UIElement element)
        {
            element.MouseLeftButtonDown += new MouseButtonEventHandler(element_MouseLeftButtonDown);
            element.MouseLeftButtonUp += new MouseButtonEventHandler(element_MouseLeftButtonUp);
            element.MouseEnter += (sender, e) =>
            {
                Wind.Cursor = Cursors.SizeNWSE;
                isMouseMoove = false;
            };
            element.MouseLeave += (sender, e) =>
            {
                Wind.Cursor = Cursors.Arrow;
                isMouseMoove = true;
            };
        }

        private void element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDoun = false;
        }

        private void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API.GetCursorPos(out last_poscur);
            resizeSize = new Size(Wind.Width, Wind.Height);
            isMouseDoun = true;
           
        }

        public void Update()
        {
            if (Mouse.LeftButton == MouseButtonState.Released)
                isMouseDoun = false;
            if (isMouseDoun)
            {
                API.GetCursorPos(out poscur);
                Width = resizeSize.x - (last_poscur.X - poscur.X);
                Height = resizeSize.y - (last_poscur.Y - poscur.Y);
            }
        }
    }
    public struct Size
    {
        public Size(double x_ , double y_)
        {
            x = x_;
            y = y_;
        }
        public double x
        {
            get;set;
        }
        public double y
        {
            get;set;
        }
    }
    public static class API
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Pos lpPoint);
        public struct Pos
        {
            public int X;
            public int Y;
            public override string ToString()
            {
                return $"x:{X} / y:{Y}";
            }
        }
    }
}

