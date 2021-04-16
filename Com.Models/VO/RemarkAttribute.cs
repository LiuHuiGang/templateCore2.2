using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Models.VO
{
    public class RemarkAttribute : Attribute
    {
        private string m_remark;
        public RemarkAttribute(string remark)
        {
            m_remark = remark;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return m_remark; }
            set { m_remark = value; }
        }
    }
}
