angular.module("umbraco").controller("NewsletterStudioContrib.FormsChecklistPrevalue.MailinglistsController",
	function ($scope, $routeParams, $http) {

	    $scope.mailinglists = [];

        // Get's all the mailing lists in Newsletter Studio using WebApi from "MailingListsController.cs"
	    $http.get("backoffice/NewsletterStudioContrib/MailingLists/GetMailingLists").success(function (data) {

	        $scope.mailinglists = data;

            // If the setting has configuration with selected lists, make sure that they are selected in the $scope.mailingslist-array.
	        if ($scope.setting.value != undefined) {

	            var arrSelected = $scope.setting.value.split(',');

	            angular.forEach($scope.mailinglists, function (value) {
                    // Is the name of the current maling list in the array of selected lists? (arrSelected)
	                if (arrSelected.indexOf(value.name) != -1) {
	                    value.selected = true;
	                }
	            });   
	        }
	    });

	    
	    $scope.toggleSelection = function(prevalue) {

	        // set the value for selected to the opposite
	        prevalue.selected = !prevalue.selected;
            
	        // update the $scope.setting.value-property to reflect the current selection.
	        var strValue = "";
	        var hitCount = 0;
	        for (var i = 0; i < $scope.mailinglists.length; i++) {

	            if ($scope.mailinglists[i].selected) {
	                if (hitCount > 0) {
	                    strValue = strValue + ",";
	                }
	                strValue = strValue + $scope.mailinglists[i].name;
	                hitCount++;
	            }
	        }

            // update the scope only here, strValue keeps the value during the loop to avoid updating the scope during the loop.
	        $scope.setting.value = strValue;

	    };

});