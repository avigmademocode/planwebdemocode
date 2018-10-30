HomeApp.controller('SupportingEmailsController', ['$scope', '$window', 'SupportingEmailsFactory', 'NewRequestFactory', function ($scope, $window, SupportingEmailsFactory, NewRequestFactory) {

    $scope.GetCustomersList = function () {
        NewRequestFactory.GetCustomers(1)
        .then(function (response) {
            if (response.data.length != 0) {
                $scope.custList = response.data[0];
            }
        });
    }
    $scope.GetCustomersList();

    $scope.SupportingEmailsDetail = function (CustID) {
        debugger;
        if (CustID == undefined || CustID == 'undefined') {

            CustID = 0;
        }
        SupportingEmailsFactory.SupportingEmailsData(CustID)
             .then(function (response) {
                 if (response.data.length > 0) {
                     $scope.SupportingEmailsList = response.data[0];
                     $scope.CustEmailList = response.data[1];
                 }
             });
    }
    $scope.CustomerId = 0;
    $scope.SupportingEmailsDetail($scope.CustomerId);

    $scope.addRow = function () {
        var obj = {};
        $scope.Edit = true;
        obj.SuppEmailId = 0;
        obj.email = '';
        obj.isActive = false;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;
        $scope.SupportingEmailsList.push(obj);
    };

    $scope.SaveSupportingEmails = function (EmailList) {
        var SupportingEmailsList =
            {
                CustId: $scope.CustomerId,
                EmailData: JSON.stringify(EmailList),
                Type:1
            }
        SupportingEmailsFactory.SaveSupportingEmailsData(SupportingEmailsList)
            .then(function (response) {
                if (response.data.length > 0)
                {
                    debugger;
                    if (response.data[0] == "-99")
                    {
                        alert('Email Already Exist.');
                    }
                      else
                    {
                        $scope.SupportingEmailsList = response.data[1][0];
                        $scope.CustEmailList = response.data[1][1];
                        alert('Request has been saved successfuully.');
                    }
                }
            });
    }

    $scope.SaveStatus = function () {
        //console.log($scope.Customers)
        debugger;
        var bool = validation();
        if (bool) {


            var data =
                {
                    Email: $scope.emailID,
                    SuppEmailId: $scope.SuppEmailId,
                    Customerdet: JSON.stringify($scope.Finalcustlist),
                    Type: 2
                }
            SupportingEmailsFactory.SaveSupportingEmailsData(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        $scope.SupportingEmailsList = response.data[1][0];
                        $scope.CustEmailList = response.data[1][1];
                        if (response.data[0] == -99) {
                            alert('Email Already Exist.');
                        }
                        else {
                            alert('Request has been saved successfully.');
                        }

                    }
                });
        }

    }

    var bool = true;
    function validation() {
        bool = true;
        if ($scope.emailID == '' || $scope.emailID == 'undefined' || $scope.emailID == undefined) {
            bool = false;
            alert('Please Enter Valid Email ID');
            return bool;
        }

        bool = false;
        for (var i = 0; i < $scope.Finalcustlist.length; i++) {
            if ($scope.Finalcustlist[i].IsCat == true) {
                bool = true;

                return bool;
            }
            else {
                bool = false;
            }

        }
        if (!bool) {
            alert('Please Select Atleast one Customer');
            return bool;
        }
        return bool;
    }

    $scope.Checked = function () {
        for (var i = 0; i <= $scope.SupportingEmailsList.length; i++) {
            $scope.SupportingEmailsList[i].IsEdit = false;
            $scope.SupportingEmailsList[i].Ischange = 0;

        }
        $scope.ResetData();
    }


    $scope.ResetData = function () {
        debugger;
        $scope.emailID = '';
        $scope.SuppEmailId = 0;
        $scope.Finalcustlist = [];
        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            $scope.Finalcustlist.push(obj);
        }
    }

    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }
    $scope.EditStatus = function (SupportingEmails) {
        debugger;

        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];

        $scope.SuppEmailId = SupportingEmails.SuppEmailId;
        $scope.emailID = SupportingEmails.email;

        for (var i = 0; i < $scope.CustEmailList.length; i++)
        {
            if ($scope.CustEmailList[i].SuppEmailId == SupportingEmails.SuppEmailId)
            {

                var obj = {};
                obj.CustId = $scope.CustEmailList[i].CustId;
                obj.IsCat = true;
                $scope.tempcustlist.push(obj);


            }

        }

        for (var i = 0; i < $scope.custList.length; i++)
        {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++)
            {
                if ($scope.custList[i].CustId == $scope.tempcustlist[j].CustId)
                {
                    obj.IsCat = true;
                }

            }

            $scope.Finalcustlist.push(obj);
        }




    }






}]);