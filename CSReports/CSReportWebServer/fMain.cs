﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CSReportWebServer
{
    public partial class fMain : Form
    {
        private string[] m_args;

        private Dictionary<string, Report> m_reports = new Dictionary<string, Report>();

        public fMain(string[] args)
        {
            InitializeComponent();
            m_args = args;
        }

        private void cmdRegister_Click(object sender, EventArgs e)
        {
            Main.RegisterNativeMessagingHost(new string[] { "register" });
        }

        delegate void LogCallback(string message);

        private void safeLog(string message)
        {
            var i = lvLog.Items.Add(DateTime.Now.ToString("h:mm:ss tt"));
            i.SubItems.Add(message);
        }

        public void log(string message)
        {
            LogCallback d = new LogCallback(safeLog);
            this.Invoke(d, new object[] { message });
        }

        delegate void CloseCallback();

        public void close() {
            CloseCallback d = new CloseCallback(this.Close);
            this.Invoke(d);
        }

        delegate void ReportActionCallback(JObject request);

        private void safePreview(JObject request)
        {
            var pathAndFile = @"C:\proyectos\CSReportes\CSReportes\Informes\Reportes\" + request["message"]["data"]["file"];
            var report = new Report();
            report.init(request);
            if (report.openDocument(pathAndFile))
            {
                report.preview();
            }
            m_reports.Add(report.reportId, report);
        }

        private void safeMoveToPage(JObject request)
        {
            var data = request["message"]["data"];
            var reportId = data["reportId"].ToString();
            var page =  int.Parse(data["page"].ToString());
            var report = m_reports[reportId];
            report.moveToPage(page);
        }

        public void preview(JObject request)
        {
            ReportActionCallback d = new ReportActionCallback(safePreview);
            this.Invoke(d, new object[] { request });
        }

        public void moveToPage(JObject request)
        {
            ReportActionCallback d = new ReportActionCallback(safeMoveToPage);
            this.Invoke(d, new object[] { request });
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            Main.Init(m_args, this);
        }
    }
}
