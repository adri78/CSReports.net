﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CSKernelClient;

namespace CSReportDll
{

    public class cReportPage
    {

        private const String C_MODULE = "cReportPage";

        private const String C_NODERPTHEADER = "Header";
        private const String C_NODERPTHEADERLINE = "HeaderLine";
        private const String C_NODERPTDETAIL = "Detail";
        private const String C_NODERPTDETAILLINE = "DetailLine";
        private const String C_NODERPTFOOTER = "Footer";
        private const String C_NODERPTFOOTERLINE = "FooterLine";

        private cReportPageFields m_detail = new cReportPageFields();
        private cReportPageFields m_header = new cReportPageFields();
        private cReportPageFields m_footer = new cReportPageFields();
        private int m_pageNumber = 0;

        private float m_headerBottom = 0;
        private float m_footerTop = 0;

        public cReportPageFields getHeader()
        {
            return m_header;
        }

        public void setHeader(cReportPageFields rhs)
        {
            m_header = rhs;
        }

        public cReportPageFields getDetail()
        {
            return m_detail;
        }

        public void setDetail(cReportPageFields rhs)
        {
            m_detail = rhs;
        }

        public cReportPageFields getFooter()
        {
            return m_footer;
        }

        public void setFooter(cReportPageFields rhs)
        {
            m_footer = rhs;
        }

        public int getPageNumber()
        {
            return m_pageNumber;
        }

        public void setPageNumber(int rhs)
        {
            m_pageNumber = rhs;
        }

        public float getHeaderBottom()
        {
            return m_headerBottom;
        }

        public void setHeaderBottom(float rhs)
        {
            m_headerBottom = rhs;
        }

        public float getFooterTop()
        {
            return m_footerTop;
        }

        public void setFooterTop(float rhs)
        {
            m_footerTop = rhs;
        }

        internal bool load(CSXml.cXml xDoc, XmlNode nodeObj)
        {
            XmlNode nodeObjSecLn = null;

            m_pageNumber = xDoc.getNodeProperty(nodeObj, "PageNumber").getValueInt(eTypes.eInteger);
            m_headerBottom = xDoc.getNodeProperty(nodeObj, "HeaderBottom").getValueInt(eTypes.eLong);
            m_footerTop = xDoc.getNodeProperty(nodeObj, "FooterTop").getValueInt(eTypes.eLong);

            m_header.clear();
            m_detail.clear();
            m_footer.clear();

            nodeObj = xDoc.getNodeFromNode(nodeObj, C_NODERPTHEADER);
            if (xDoc.nodeHasChild(nodeObj))
            {
                nodeObjSecLn = xDoc.getNodeChild(nodeObj);
                while (nodeObjSecLn != null)
                {
                    if (!m_header.add(null).load(xDoc, nodeObjSecLn)) 
                    { 
                        return false; 
                    }
                    nodeObjSecLn = xDoc.getNextNode(nodeObjSecLn);
                }
            }

            nodeObj = xDoc.getNodeFromNode(nodeObj, C_NODERPTDETAIL);
            if (xDoc.nodeHasChild(nodeObj))
            {
                nodeObjSecLn = xDoc.getNodeChild(nodeObj);
                while (nodeObjSecLn != null)
                {
                    if (!m_detail.add(null).load(xDoc, nodeObjSecLn)) 
                    { 
                        return false; 
                    }
                    nodeObjSecLn = xDoc.getNextNode(nodeObjSecLn);
                }
            }

            nodeObj = xDoc.getNodeFromNode(nodeObj, C_NODERPTFOOTER);
            if (xDoc.nodeHasChild(nodeObj))
            {
                nodeObjSecLn = xDoc.getNodeChild(nodeObj);
                while (nodeObjSecLn != null)
                {
                    if (!m_footer.add(null).load(xDoc, nodeObjSecLn)) 
                    { 
                        return false; 
                    }
                    nodeObjSecLn = xDoc.getNextNode(nodeObjSecLn);
                }
            }

            return true;

        }

        internal bool save(CSXml.cXml xDoc, XmlNode nodeFather)
        {
            CSXml.cXmlProperty xProperty = null;
            XmlNode nodeObj = null;

            xProperty = new CSXml.cXmlProperty();

            xProperty.setName("Page");
            nodeObj = xDoc.addNodeToNode(nodeFather, xProperty);

            xDoc.setNodeText(nodeObj, "Página " + m_pageNumber);

            xProperty.setName("PageNumber");
            xProperty.setValue(eTypes.eInteger, m_pageNumber);
            xDoc.addPropertyToNode(nodeObj, xProperty);

            xProperty.setName("HeaderBottom");
            xProperty.setValue(eTypes.eLong, m_headerBottom);
            xDoc.addPropertyToNode(nodeObj, xProperty);

            xProperty.setName("FooterTop");
            xProperty.setValue(eTypes.eLong, m_footerTop);
            xDoc.addPropertyToNode(nodeObj, xProperty);

            cReportPageField pageFld = null;
            XmlNode nodeAux = null;

            xProperty.setName(C_NODERPTHEADER);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_header.count(); _i++)
            {
                pageFld = m_header.item(_i);
                pageFld.save(xDoc, nodeAux);
            }

            xProperty.setName(C_NODERPTDETAIL);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_detail.count(); _i++)
            {
                pageFld = m_detail.item(_i);
                pageFld.save(xDoc, nodeAux);
            }

            xProperty.setName(C_NODERPTFOOTER);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_footer.count(); _i++)
            {
                pageFld = m_footer.item(_i);
                pageFld.save(xDoc, nodeAux);
            }

            return true;
        }

        internal bool saveForWeb(CSXml.cXml xDoc, XmlNode nodeFather)
        {
            CSXml.cXmlProperty xProperty = null;
            XmlNode nodeObj = null;

            xProperty = new CSXml.cXmlProperty();

            xProperty.setName("Page");
            nodeObj = xDoc.addNodeToNode(nodeFather, xProperty);

            xDoc.setNodeText(nodeObj, "Página " + m_pageNumber);

            cReportPageField pageFld = null;
            XmlNode nodeAux = null;
            float top = 0;
            bool addLine = false;

            int nHeader = 0;

            xProperty.setName(C_NODERPTHEADER);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_header.count(); _i++)
            {
                pageFld = m_header.item(_i);
                addLine = false;

                if (pageFld.getTop() == 0)
                {
                    if (top != pageFld.getInfo().getAspect().getTop())
                    {
                        top = pageFld.getInfo().getAspect().getTop();
                        addLine = true;
                        nHeader = nHeader + 1;
                    }
                }
                else
                {
                    if (top != pageFld.getTop())
                    {
                        top = pageFld.getTop();
                        addLine = true;
                        nHeader = nHeader + 1;
                    }
                }

                if (addLine)
                {
                    xProperty.setName(C_NODERPTHEADERLINE + nHeader.ToString());
                    xProperty.setValue(eTypes.eText, "");
                    nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);
                }

                pageFld.saveForWeb(xDoc, nodeAux);
            }

            xProperty.setName(C_NODERPTDETAIL);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_detail.count(); _i++)
            {
                pageFld = m_detail.item(_i);

                addLine = false;

                if (pageFld.getTop() == 0)
                {
                    if (top != pageFld.getInfo().getAspect().getTop())
                    {
                        top = pageFld.getInfo().getAspect().getTop();
                        addLine = true;
                    }
                }
                else
                {
                    if (top != pageFld.getTop())
                    {
                        top = pageFld.getTop();
                        addLine = true;
                    }
                }

                if (addLine)
                {
                    xProperty.setName(C_NODERPTDETAILLINE);
                    xProperty.setValue(eTypes.eText, "");
                    nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);
                }

                pageFld.saveForWeb(xDoc, nodeAux);
            }

            xProperty.setName(C_NODERPTFOOTER);
            xProperty.setValue(eTypes.eText, "");
            nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);

            for (int _i = 0; _i < m_footer.count(); _i++)
            {
                pageFld = m_footer.item(_i);
                addLine = false;

                if (pageFld.getTop() == 0)
                {
                    if (top != pageFld.getInfo().getAspect().getTop())
                    {
                        top = pageFld.getInfo().getAspect().getTop();
                        addLine = true;
                    }
                }
                else
                {
                    if (top != pageFld.getTop())
                    {
                        top = pageFld.getTop();
                        addLine = true;
                    }
                }

                if (addLine)
                {
                    xProperty.setName(C_NODERPTFOOTERLINE);
                    xProperty.setValue(eTypes.eText, "");
                    nodeAux = xDoc.addNodeToNode(nodeObj, xProperty);
                }

                pageFld.saveForWeb(xDoc, nodeAux);
            }

            return true;
        }

    }

}
