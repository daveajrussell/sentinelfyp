var CreateUser = {

	Vars: {
		Username: "",
		Roles: "",
		FirstName: "",
		LastName: "",
		Number: "",
		Email: ""
	},

	Init: function () {
		var self = this;

		$(function () {

			$('#btnCreateUser').live('click', function () {

				var validUsername = self.ValidateUsername();
				var validRoles = self.ValidateRoles();
				var validFirstname = self.ValidateFirstName();
				var validLastname = self.ValidateLastName();
				var validNumber = self.ValidateContactNumber();
				var validEmail = self.ValidateEmail();

				if (validUsername && validRoles && validFirstname && validLastname && validNumber && validEmail) {
					$.post('CreateUser', { strUsername: self.Vars.Username, strKeys: self.Vars.Roles.toString(), strFirstName: self.Vars.FirstName, strLastName: self.Vars.LastName, strNumber: self.Vars.Number, strEmail: self.Vars.Email }, function (data) {
						$('#site-section').append(data);
					});
				}
			});
		});
	},




	ValidateUsername: function () {
		if ($('#UserName').val() == '') {
			if (!$('#UserName').hasClass('input-error')) {
				$('#UserName').addClass('input-error');
				$('#UserName').parent().append('<span id="username-error" class="ui-icon ui-icon-alert"></span>');

				$('#username-error').qtip({
					content: 'Username Is Required',
					style: {
						classes: 'qtip-red'
					}
				});

				return false;
			}
			return false;
		}
		else {
			$('#UserName').removeClass('input-error');
			$('#UserName').parent().find('.ui-icon').remove();

			this.Vars.Username = $('#UserName').val();

			return true;
		}
	},

	ValidateRoles: function () {
		var self = this;
		self.Vars.Roles = new Array();

		$('input[type=checkbox]:checked').each(function () {
			self.Vars.Roles.push($(this).val());
		});

		if (self.Vars.Roles.length == 0) {
			if (!$('.Roles').hasClass('input-error')) {
				$('.Roles').addClass('input-error');
				$('.Roles').parent().append('<span id="roles-error" class="ui-icon ui-icon-alert"></span>');

				$('#roles-error').qtip({
					content: 'At Least One Role Is Required',
					style: {
						classes: 'qtip-red'
					}
				});
				return false;
			}
			return false
		}
		else {
			$('.Roles').removeClass('input-error');
			$('.Roles').parent().find('.ui-icon').remove();

			return true;
		}
	},

	ValidateFirstName: function () {
		if ($('#FirstName').val() == '') {
			if (!$('#FirstName').hasClass('input-error')) {
				$('#FirstName').addClass('input-error');
				$('#FirstName').parent().append('<span id="firstname-error" class="ui-icon ui-icon-alert"></span>');

				$('#firstname-error').qtip({
					content: 'First Name Is Required',
					style: {
						classes: 'qtip-red'
					}
				});
				return false;
			}
			return false;
		}
		else {
			$('#FirstName').removeClass('input-error');
			$('#FirstName').parent().find('.ui-icon').remove();

			this.Vars.FirstName = $('#FirstName').val();

			return true;
		}
	},

	ValidateLastName: function () {
		if ($('#LastName').val() == '') {
			if (!$('#LastName').hasClass('input-error')) {
				$('#LastName').addClass('input-error');
				$('#LastName').parent().append('<span id="lastname-error" class="ui-icon ui-icon-alert"></span>');

				$('#lastname-error').qtip({
					content: 'Last Name Is Required',
					style: {
						classes: 'qtip-red'
					}
				});
				return false;
			}
			return false;
		}
		else {
			$('#LastName').removeClass('input-error');
			$('#LastName').parent().find('.ui-icon').remove();

			this.Vars.LastName = $('#LastName').val();

			return true;
		}
	},

	ValidateContactNumber: function () {
		if ($('#UserContactNumber').val() == '') {
			if (!$('#UserContactNumber').hasClass('input-error')) {
				$('#UserContactNumber').addClass('input-error');
				$('#UserContactNumber').parent().append('<span id="usernumber-error" class="ui-icon ui-icon-alert"></span>');

				$('#usernumber-error').qtip({
					content: 'User Contact Number Is Required',
					style: {
						classes: 'qtip-red'
					}
				});
				return false;
			}
			return false;
		}
		else {
			$('#UserContactNumber').removeClass('input-error');
			$('#UserContactNumber').parent().find('.ui-icon').remove();

			this.Vars.Number = $('#UserContactNumber').val();

			return true;
		}
	},

	ValidateEmail: function () {
		if ($('#Email').val() == '') {
			if (!$('#Email').hasClass('input-error')) {
				$('#Email').addClass('input-error');
				$('#Email').parent().append('<span id="useremail-error" class="ui-icon ui-icon-alert"></span>');

				$('#useremail-error').qtip({
					content: 'User Email Is Required',
					style: {
						classes: 'qtip-red'
					}
				});
				return false;
			}
			return false;
		}
		else {
			$('#Email').removeClass('input-error');
			$('#Email').parent().find('.ui-icon').remove();

			this.Vars.Email = $('#Email').val();

			return true;
		}
	}
};

CreateUser.Init();