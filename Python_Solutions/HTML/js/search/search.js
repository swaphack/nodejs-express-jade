/**
 * 搜索相关函数集
 */
var qry = qry || {};

/**
 * 禁用回车键搜索
 */
qry.forbidEnter = function(form) {
	$(form).find(":input").keypress(function(e) {
		var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
		// 禁用回车
		if(keyCode == 13) {
			return false;			
		}
	});
};

/**
 * 根据关键词搜索结果
 * 
 * @param kw 关键词输入框元素表达式
 * @param type 搜索类型：1-企业信用信息、2-经营异常名录、3-严重违法企业名单、4-抽查检查信息、5-信息公告 6-其他部门行政许可 7-其他部门行政处罚
 * @param ifCheck 是否需要校验并提示
 * @param ifCaptcha 是否需要验证码
 */
qry.search = function(kw, type, ifCheck ,ifCaptcha) {
	// 搜索关键字（企业名称或注册号）
	var keyword = $(kw).val();
	// 设置搜索类型
	$("#searchType").val(type);
	// 对于企业信用信息查询，关键词不能为空
	if(ifCheck == constants.yes && ($.trim(keyword) == "" ||$.trim(keyword) == "请输入企业名称或注册号或统一社会信用代码" )){
		window.top.dialogMessage("企业名称或注册号或统一社会信用代码不能为空！");
		return false;
	}else if ( $.trim(keyword) == "" ||$.trim(keyword) == "请输入企业名称或注册号或统一社会信用代码") {
			keyword = "";
			$(kw).attr("value", "");
	}
	// 检查当前IP是否处于黑名单
	ajaxCommon({
		url: "security/verify_ip",
		async: false
	}, function(data){
		if(data == constants.yes) {
			// 检查输入的关键词是否符合要求
			ajaxCommon({
				url: "security/verify_keyword",
				data: {
					"keyword": keyword
				}
			}, function(data) {
				if(data == constants.yes) {
					if(ifCaptcha == constants.yes) {
						$("#pageNo").val(1);//重置页码
						// 弹出验证码窗口
						qry.popupCaptcha();
					} else {
						var srform = $("#formInfo");
						// 根据查询类型指定查询方法
						switch(type) {
							case "1":
								srform.attr("action", "search/ent_info_list");
								break;
							case "2":
								srform.attr("action", "search/ent_except_list");
								break;
							case "3":
								srform.attr("action", "search/ent_black_list");
								break;
							case "4":
								srform.attr("action", "search/ent_spot_check_list");
								break;
							case "5":
								srform.attr("action", "search/ent_announce");
								break;
							default:
								break;
						};
						// 提交查询
						srform.submit();
					}
				} else {
					window.top.dialogMessage("请输入更为详细的查询条件！");
				}
			});
		} else {
			window.top.dialogMessage("当前IP段不能进行查询操作！");
		}
	});
};

/**
 * 弹出窗口默认值
 */
qry.popupDefault = {
	// iframe对象ID
	id:	"dialog-iframe-search"
};

/**
 * 弹出验证码窗口
 * 
 * @param o 配置项
 */
qry.popupCaptcha = function(o) {
	// 函数对象引用
	var _self = this;

	// 函数默认配置
	this._options = {
		// 验证码窗口高度
		height: 370,
		// 验证码窗口宽度
		width: 438,
		// 验证码窗口容器样式
		css: {
			"padding": 0,
			"background-color": "#eceeee",
			"border": "#c3d4d1 1px solid"
		},
		// 验证码窗口iframe样式
		framecss: {
			"background-color": "#eceeee"
		},
		// 验证码窗口遮挡层样式
		overlaycss: {
			"background-color": "#000",
			"background-image": "none",
			"opacity": "0",
			"filter": "Alpha(Opacity=0)"
		}
	};

	// 继承配置项
	$.extend(true, this._options, qry.popupDefault);
	$.extend(true, this._options, o);
	
	// 弹出窗口
	dialogIframe($.extend(true, {
		url: $("base").attr("href") + "search/popup_captcha",
		scrolling: "no",
		titlebar: false,
		wrapper: false,
		buttons: null
	}, _self._options));
};

/**
 * 关闭弹出窗口
 * 
 * @param fid 弹出窗口iframe对象ID
 */
qry.popupClose = function(fid) {
	// 如果未指定弹出窗口iframe对象ID，则读取默认值
	fid = typeof fid == "undefined" ? qry.popupDefault.id : fid;

	// 调用弹出窗口关闭函数，关闭窗口并释放资源
	window.top.dialogIframeClose(fid);
};