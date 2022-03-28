using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService
{
   
    public partial class Service : ServiceBase
    {
        private bool _cancel;
        private bool _enabled;
        public Service()
        {
           
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            
            _cancel = false;

            Task.Run(() => Processing());
            


            EventLog.WriteEntry("Started", EventLogEntryType.Information);
        }
        public void Processing()
        { 
           
            try
            {
                while (!_cancel)
                {
                    ServiceController[] scServices;
                    
                    scServices = ServiceController.GetServices();
                   
                    foreach (ServiceController scTemp in scServices)
                    {
                        if ((scTemp.Status == ServiceControllerStatus.Stopped || scTemp.Status == ServiceControllerStatus.StopPending)
                                && (scTemp.ServiceName == "TestService"  || scTemp.ServiceName == "TestService1"))
                        {
                            scTemp.Start();
                            //scTemp.WaitForStatus(ServiceControllerStatus.Running);
                            scTemp.Refresh();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnStop()
        {
            
            EventLog.WriteEntry("Stoped", EventLogEntryType.Information);
        }
    }
}
