﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSReportGlobals;

namespace CSReportDll
{
    public class cReportFormulaInt
    {

        private const String C_MODULE = "cReportFormulaInt";

        private cReportVariables m_variables = new cReportVariables();
        private cReportFormulaParameters m_parameters = new cReportFormulaParameters();
        private csRptFormulaType m_formulaType = 0;

        internal cReportVariables getVariables()
        {
            return m_variables;
        }

        internal cReportFormulaParameters getParameters()
        {
            return m_parameters;
        }

        public csRptFormulaType getFormulaType()
        {
            return m_formulaType;
        }

        public void setFormulaType(csRptFormulaType rhs)
        {
            m_formulaType = rhs;
        }

    }

}
