HomeApp.controller('ManageCustomerController', ['$scope', '$window', 'NewRequestFactory', 'ManageCustomerFactory', function ($scope, $window, NewRequestFactory, ManageCustomerFactory) {

    $scope.IsEditHide = true;
    $scope.IsSaveHide = true;
    $scope.CustName = '';
    $scope.CustID = 0;
    $scope.IsgrantbtnHide = false;
    $scope.IsBudgetbtnHide = true;
    $scope.GetCustomersList = function () {
        debugger;
        NewRequestFactory.GetCustomers(3)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                    
                    debugger;
                    if ($scope.CustID != 0)
                    {
                        var index = $scope.custList.findIndex(obj => obj.CustId == $scope.CustID);
                        if (index >= 0)
                        {
                            $scope.selectedCustomer = $scope.custList[index].CustId;
                        }
                    }



                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.Add = function () {
        $scope.IsEditHide = true;
        $scope.IsSaveHide = false;

        ResetData();
        
    }
    $scope.setCustomersList = function (val) {
        debugger;
        $window.localStorage.removeItem('MCustID');
        $window.localStorage.setItem('MCustID', val);

        $scope.IsEditHide = false;
        $scope.IsSaveHide = true;
   
        for (var i = 0; i < $scope.custList.length; i++)
        {
            if ($scope.custList[i].CustId == val)
            {
                if ($scope.custList[i].UseItemGroups)
                {
                    $scope.IsgrantbtnHide = false;
                    $scope.IsBudgetbtnHide = true;
                }
                else
                {
                    $scope.IsBudgetbtnHide = false;

                    $scope.IsgrantbtnHide = true;
                }
                
                break;
            }
        }

        ManageCustomerFactory.GetCustomerData(val)
            .then(function (response) {
                debugger;

                $scope.form.custNameg = response.data[0][0].CustName;
                $scope.form.Acronym = response.data[0][0].Acronym;
                $scope.form.IsActive = response.data[0][0].isActive;
                $scope.form.Code = response.data[0][0].Code;
                $scope.form.Ticker = response.data[0][0].Ticker;
                $scope.form.Demo = response.data[0][0].InDemo;
                $scope.form.TieredPricing = response.data[0][0].TieredPricing;
                $scope.CustID = response.data[0][0].CustId;
              //  console.log(response.data[0]);
            });

    }

    $scope.submitForm = function (val) {
        //formData = $scope.form;
        //alert(JSON.stringify(formData));
        debugger;
       
        var custid;
        var strtype;
        if (val == 1) {
            custid = $scope.selectedCustomer;
            strtype = 2;
        }
        else {
            custid = 0;
            strtype = 1;
        }
        var Data = {
            CustId: custid,
            CustName: $scope.form.custNameg,
            Acronym: $scope.form.Acronym,
            isActive: $scope.form.IsActive,
            Code: $scope.form.Code,
            Ticker: $scope.form.Ticker,
            InDemo: $scope.form.Demo,
            TieredPricing: $scope.form.TieredPricing,
            Type: strtype
        };
       // alert(JSON.stringify(Data));
        validation();
        if (bool) {


            try {
                ManageCustomerFactory.SaveCustomerData(Data)
                    .then(function (response) {
                        debugger;
                        if (response.data.length != 0) {
                            if (response.data[0] == -99)
                            {
                                alert('Customer Name  Already Exists.');
                            }
                            else if (response.data[0] == -98) {
                                alert('Acronym Name Already Exists..');
                            }
                            else if (response.data[0] == -97) {
                                alert('Code Name Already Exists..');
                            }
                            else {

                                alert('Request has been saved successfully.');
                                if (val == 0) {
                                    $scope.custList = response.data[3][0];
                                    $scope.custid = response.data[1];
                                    $scope.custname = response.data[2];

                                    if ($scope.custid != 0) {
                                        var index = $scope.custList.findIndex(obj => obj.CustId == $scope.custid);
                                        if (index >= 0) {
                                            $scope.selectedcustomer = $scope.custList[index].CustId;
                                        }
                                    }
                                    ResetData();
                                }
                            }
                         

                           
                        }

                    });
            }
            catch (e) {
                alert(e);
            }

        }
    };

    var bool = true;

    function validation()
    {
        bool = true;
        if ($scope.form.custNameg == '' || $scope.form.custNameg == undefined || $scope.form.custNameg == 'undefined')
        {
            alert('Please Enter Customer Name');
            bool = false;
            return bool;

        }
        if ($scope.form.Acronym == '' || $scope.form.Acronym == undefined || $scope.form.Acronym == 'undefined') {
            alert('Please Enter Acronym Name');
            bool = false;
            return bool;
        }

        //if ($scope.form.Code == '' || $scope.form.Code == undefined || $scope.form.Code == 'undefined') {
        //    alert('Please Enter Code');
        //    bool = false;
        //    return bool;
        //}
        return bool;
    }


    function ResetData()
    {
        $scope.form.custNameg = '';
        $scope.form.Acronym = '';
        $scope.form.IsActive = '';
        $scope.form.Code = '';
        $scope.form.Ticker = '';
        $scope.form.Demo = '';
        $scope.form.TieredPricing = '';
        $scope.CustID = '';
    }
    $scope.GetCustomersList();
}]);