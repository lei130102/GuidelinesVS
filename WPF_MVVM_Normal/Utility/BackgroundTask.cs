using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_MVVM_Normal.Utility
{
    public class BackgroundTask
    {
        public class UpdateUIEventArgs
        {
            /// <summary>
            /// 是否创建结束
            /// </summary>
            public bool IsFinish { get; set; }

            /// <summary>
            /// 进度
            /// </summary>
            public int Process { get; set; }

            //其他自定义参数
        }

        public event EventHandler<UpdateUIEventArgs> UpdateUI;

        public void Start()
        {
            Thread t = new Thread(ThreadStart);
            t.Start();
        }

        private void ThreadStart()
        {
            try
            {
                for(int idx = 1; idx <= 10; ++idx)
                {
                    UpdateUI(this, new UpdateUIEventArgs()
                    {
                        IsFinish = ((idx == 10) ? true : false),
                        Process = idx * 10
                    });
                    Thread.Sleep(1000);
                }

                UpdateUI(this, new UpdateUIEventArgs() { 
                    IsFinish = true,
                    Process = 100
                });
            }
            catch(Exception ex)
            {
                UpdateUI(this, new UpdateUIEventArgs() { 
                    IsFinish = true,
                    Process = 100
                });
            }
        }
    }
}
