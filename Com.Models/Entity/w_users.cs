using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entity
{
   public class w_users
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int? id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码（密码生成方式：md5('密码')）
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 账号总金币
        /// </summary>
        public decimal? total_coin { get; set; }
        /// <summary>
        /// 账号可用金币
        /// </summary>
        public decimal? surplus_coin { get; set; }
        /// <summary>
        /// 账号累计积分
        /// </summary>
        public decimal? total_integral { get; set; }
        /// <summary>
        /// 当前可用积分
        /// </summary>
        public decimal? surplus_integral { get; set; }
        /// <summary>
        /// 平台抽成比率（平台从卖家销售分得的比率）
        /// </summary>
        public decimal? divide_rate { get; set; }
        /// <summary>
        /// 所在公司名称
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 公司担任职位
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public int? last_login_time { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public int? last_login_ip { get; set; }
        /// <summary>
        /// 是否实名认证 1、已认证 
        /// </summary>
        public int? is_certificate { get; set; }
        /// <summary>
        /// 认证类型 1、个人 2、企业
        /// </summary>
        public int? certificate_type { get; set; }
        /// <summary>
        /// 是否卖家 1、表示为卖家身份
        /// </summary>
        public int? is_seller { get; set; }
        /// <summary>
        /// 认证地区省ID
        /// </summary>
        public int? address_id { get; set; }
        /// <summary>
        /// 账号注册时间
        /// </summary>
        public int? dateline { get; set; }
        /// <summary>
        /// 上传文件存储路径
        /// </summary>
        public string savepath { get; set; }
        /// <summary>
        /// 总空间使用增量大小，0位不限量
        /// </summary>
        public Int64? interspace_max { get; set; }
        /// <summary>
        /// 总空间使用增量
        /// </summary>
        public Int64? interspace_now { get; set; }
        /// <summary>
        /// 买家级别
        /// </summary>
        public int? buyer_level { get; set; }
        /// <summary>
        /// 卖家级别
        /// </summary>
        public int? seller_level { get; set; }
        /// <summary>
        /// 是否内部账号 0 不是1是
        /// </summary>
        public int? is_self { get; set; }
        /// <summary>
        /// 提现类型（0支付宝  1 银行卡）
        /// </summary>
        public int? cashtype { get; set; }
        /// <summary>
        /// 提现详细方式
        /// </summary>
        public string cashinfo { get; set; }
        /// <summary>
        /// 银行卡号/支付宝号(提现用)
        /// </summary>
        public string cash { get; set; }
        /// <summary>
        /// 名称（单位或姓名）（提现和申请开票用）
        /// </summary>
        public string unionname { get; set; }
        /// <summary>
        /// 开户行及银行账号（申请开票用）
        /// </summary>
        public string bankname { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string tax { get; set; }
        /// <summary>
        /// 地址及电话
        /// </summary>
        public string tax_address { get; set; }
        /// <summary>
        /// 收件人（寄送发票用）
        /// </summary>
        public string rec_user { get; set; }
        /// <summary>
        /// 收件详细地址
        /// </summary>
        public string rec_addr { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string rec_phone { get; set; }
        /// <summary>
        /// 可开具发票金额，在线支付部分可开具发票的额度
        /// </summary>
        public float? remain_sum { get; set; }
        /// <summary>
        /// 推荐人id
        /// </summary>
        public int? inviterId { get; set; }
        /// <summary>
        /// 推荐人
        /// </summary>
        public string inviter { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string truename { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public int? update_time { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? del_flag { get; set; }
        /// <summary>
        /// 是否为短视频版权方
        /// </summary>
        public int? is_shortvideo_producer { get; set; }
        /// <summary>
        /// 是否为短视频购买方
        /// </summary>
        public int? is_shortvideo_buyer { get; set; }
        /// <summary>
        /// 购买短视频的单价
        /// </summary>
        public decimal? shortvideo_price { get; set; }
        /// <summary>
        /// 购买短视频的确认函
        /// </summary>
        public string shortvideo_confirm { get; set; }
        /// <summary>
        /// mssql id
        /// </summary>
        public int? msid { get; set; }
        /// <summary>
        /// 是否为素材业务用户，0不是，1提供者，2使用者
        /// </summary>
        public int? is_mt_user { get; set; }
        /// <summary>
        /// 单位id
        /// </summary>
        public int? company_id { get; set; }
        /// <summary>
        /// 是否为内部业务人员账号，1是业务员，2客服，10上传组
        /// </summary>
        public int? is_salesman { get; set; }
        /// <summary>
        /// 内部业务员父级id
        /// </summary>
        public int? sales_parentid { get; set; }
        /// <summary>
        /// 微信openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 业务员数据授权
        /// </summary>
        public string saleman_auth { get; set; }
        /// <summary>
        /// 素材选购业务基础价格
        /// </summary>
        public decimal? mt_user_price { get; set; }
        /// <summary>
        /// 发票类型：0-未设置，1-普通发票，2-增值税专用发票
        /// </summary>
        public int? mt_invoice_type { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal? mt_tax_rate { get; set; }
        /// <summary>
        /// 淘剧淘用户Id
        /// </summary>
        public int? tjtid { get; set; }
        /// <summary>
        /// 是否是淘剧淘用户
        /// </summary>
        public int? is_tjt { get; set; }
        /// <summary>
        /// 淘剧淘-注册身份 1-买剧，2-卖剧
        /// </summary>
        public string tjtregidentity { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string company_address { get; set; }
        /// <summary>
        /// 播出平台
        /// </summary>
        public string broadcast_platform { get; set; }
        /// <summary>
        /// 是否为淘剧淘版权方
        /// </summary>
        public int? is_tjt_saler { get; set; }
        /// <summary>
        /// 子站id
        /// </summary>
        public int? site_id { get; set; }
        /// <summary>
        /// 子站是否实名认证 1、已认证 
        /// </summary>
        public int? site_certificate { get; set; }
        /// <summary>
        /// 子站是否卖家 1、表示为卖家身份
        /// </summary>
        public int? site_seller { get; set; }
        /// <summary>
        /// 用户身份证号码
        /// </summary>
        public string user_idcard { get; set; }
        /// <summary>
        /// 注册级别 -1：未填写 0 卫视 1：省台 2：市台 3：县台 4:影视公司 5：新媒体
        /// </summary>
        public int? level { get; set; }

    }
}