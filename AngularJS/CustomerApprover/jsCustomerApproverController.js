HomeApp.controller('CustomerApproverController', ['$scope', '$window', 'CustomerApproverFactory', 'NewRequestFactory', function ($scope, $window, CustomerApproverFactory, NewRequestFactory) {

    var CustomerID = $window.localStorage.getItem('MCustID');
   // CustomerID = 1;
    $scope.GetCustomersList = function () {
        NewRequestFactory.GetCustomers(1)
        .then(function (response) {
            if (response.data.length != 0) {
                $scope.custList = response.data[0];
            }
        });
    }
    $scope.GetCustomersList();

    $scope.CustomerApproverDetails = function (CustId) {
        if (CustId != null) {
            $scope.CustomerId = parseInt(CustId);
        }
        else {
            $scope.CustomerId = parseInt(0);
        }
        CustomerApproverFactory.CustomerApproverData(CustId)
             .then(function (response) {
                 if (response.data.length > 0) {

                     $scope.CustomerApproverList = response.data[0];
                 }
             });
    }
    $scope.CustomerApproverDetails(CustomerID);

    $scope.addRow = function () {
        debugger;
        var length = $scope.CustomerApproverList.length;
        if ($scope.CustomerApproverList.length )
        {
            var level = $scope.CustomerApproverList[0].LevelofAuthority;

        }
        if (length == 0)
        {
            var obj = {};
            $scope.Edit = true;
            obj.CustApproverId = 0;
            obj.ApproverSerial = '';
            obj.ApproverNameDisplay = '';
            //obj.IsActive = true;
            //obj.Type = 1;
            obj.IsEdit = $scope.Edit;
            obj.Ischange = 1;
            $scope.CustomerApproverList.push(obj);
        }
     else if (level >= length)
        {
            var obj = {};
            $scope.Edit = true;
            obj.CustApproverId = 0;
            obj.ApproverSerial = '';
            obj.ApproverNameDisplay = '';
            //obj.IsActive = true;
            //obj.Type = 1;
            obj.IsEdit = $scope.Edit;
            obj.Ischange = 1;
            $scope.CustomerApproverList.push(obj);
        }
        else
        {
            alert('Please check the Level. It should be less then ' + level);
        }
       
    };

    $scope.SaveCustomerApprover = function (ApproverList) {
        var data =
            {
                ApproverData: JSON.stringify(ApproverList),
                CustId: $scope.CustomerId,
                Type: 1
            }
        CustomerApproverFactory.SaveCustomerApproverData(data)
            .then(function (response) {
                if (response.data.length > 0) {
                    debugger;

                    $scope.CustomerApproverList = response.data[1][0];
                    alert('Request has been saved successfully.');

                }
            });
    }

    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }
}]);