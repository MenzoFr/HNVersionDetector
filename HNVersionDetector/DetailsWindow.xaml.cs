using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HelloNeighborVersionDetector
{
    public partial class DetailsWindow : Window
    {
        private static DetailsWindow instance;

        public static void ShowWindow(string timestamp, string memorySize, string process, Window owner)
        {
            if (instance == null)
            {
                instance = new DetailsWindow(timestamp, memorySize, process);
                instance.Owner = owner;
                instance.Closed += (s, e) => instance = null;
                instance.Show();
                instance.PlayPopupAnimation();
            }
            else
            {
                if (instance.WindowState == WindowState.Minimized)
                {
                    instance.WindowState = WindowState.Normal;
                    instance.PlayRestoreAnimation();
                }
                else
                {
                    instance.PlayAttentionAnimation();
                }
                instance.Activate();
                instance.Focus();
            }
        }

        private void PlayPopupAnimation()
        {
            var fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var scaleXAnimation = new DoubleAnimation
            {
                From = 0.7,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(350),
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
            };

            var scaleYAnimation = new DoubleAnimation
            {
                From = 0.7,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(350),
                EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.3 }
            };

            RootContainer.BeginAnimation(OpacityProperty, fadeIn);
            RootContainerScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, scaleXAnimation);
            RootContainerScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, scaleYAnimation);
        }

        private void PlayAttentionAnimation()
        {
            var scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1.05, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1.0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))));

            RootContainerScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, scaleAnimation);
            RootContainerScale.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, scaleAnimation);
        }

        private void PlayRestoreAnimation()
        {
            var restoreAnimation = (Storyboard)FindResource("RestoreAnimation");
            restoreAnimation.Begin(RootContainer);
        }

        private DetailsWindow(string timestamp, string memorySize, string process)
        {
            InitializeComponent();
            TimestampText.Text = timestamp;
            MemorySizeText.Text = memorySize;
            ProcessText.Text = process;

            RootContainer.Opacity = 0;
            RootContainerScale.ScaleX = 0.7;
            RootContainerScale.ScaleY = 0.7;

            this.StateChanged += (sender, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    PlayRestoreAnimation();
                }
            };
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var minimizeAnimation = (Storyboard)FindResource("MinimizeAnimation");
            minimizeAnimation.Completed += (s, args) =>
            {
                this.WindowState = WindowState.Minimized;
            };
            minimizeAnimation.Begin(RootContainer);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var closeAnimation = (Storyboard)FindResource("CloseAnimation");
            closeAnimation.Completed += (s, args) => this.Close();
            closeAnimation.Begin(RootContainer);
        }

        private void CopyTimestamp_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TimestampText.Text);
            ShowCopyFeedback(CopyTimestampButton);
        }

        private void CopyMemorySize_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(MemorySizeText.Text);
            ShowCopyFeedback(CopyMemorySizeButton);
        }

        private void CopyProcess_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ProcessText.Text);
            ShowCopyFeedback(CopyProcessButton);
        }

        private void ShowCopyFeedback(System.Windows.Controls.Button button)
        {
            string originalContent = button.Content.ToString();
            button.Content = "✓";
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (s, e) =>
            {
                button.Content = originalContent;
                timer.Stop();
            };
            timer.Start();
        }
    }
}