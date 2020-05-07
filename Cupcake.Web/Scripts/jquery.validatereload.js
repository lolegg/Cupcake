jQuery.validator.addMethod('notEqualTo', function (value, element, param) {
	//意思是表单值为空时也能通过验证
	//但，如果表单有值，就必须满足||后面的条件，否则返回false
	return this.optional(element) || value != $(element).attr("data-val-notequalto-other");
});

//第一个参数是jquery验证扩展方法名
//第二个参数与rule.ValidationParameters["other"]中的key对应
//option是指ModelClientValidationRule对象实例
jQuery.validator.unobtrusive.adapters.add('notequalto', ['other'], function (options) {
	options.rules['notEqualTo'] = '#' + options.params.other;
	if (options.message) {
		options.messages['notEqualTo'] = options.message;
	}
});

jQuery.validator.addMethod('newphonemobile', function (value, element, param) {
	var revalue = this.optional(element);
	if (!revalue) {
		var myreg = /^\d{11}$/;
		if (!myreg.test(value)) {
			revalue = false;
		}
		else
			revalue = true;
	}
	return revalue;
});

jQuery.validator.unobtrusive.adapters.add('newphonemobile', function (options) {
	options.rules['newphonemobile'] = 'newphonemobile';
	if (options.message) {
		options.messages['newphonemobile'] = options.message;
	}
});

jQuery.validator.addMethod('newemail', function (value, element, param) {
	var revalue = this.optional(element);

	if (!revalue) {
		var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
		if (!myreg.test(value)) {
			revalue = false;
		}
		else {
			revalue = true;
		}
	}
	return revalue;
});

jQuery.validator.unobtrusive.adapters.add('newemail', function (options) {
	options.rules['newemail'] = 'newemail';
	if (options.message) {
		options.messages['newemail'] = options.message;
	}
});


jQuery.validator.addMethod('newidcard', function (value, element, param) {
	var revalue = this.optional(element);

	if (!revalue) {
		var myreg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
		if (!myreg.test(value)) {
			revalue = false;
		}
		else {
			revalue = true;
		}
	}
	return revalue;
});

jQuery.validator.unobtrusive.adapters.add('newidcard', function (options) {
	options.rules['newidcard'] = 'newidcard';
	if (options.message) {
		options.messages['newidcard'] = options.message;
	}
});
