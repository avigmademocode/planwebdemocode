HomeApp.controller('CustomerSettingsController', ['$scope', '$window', 'NewRequestFactory', 'CustomerSettingsFactory', function ($scope, $window, NewRequestFactory ,CustomerSettingsFactory) {

    
    var CustomerID = $window.localStorage.getItem('MCustID');
   
    $scope.ref = [
        { id: false, name: "User Entered" },
        { id: true, name: "System Created" }
    ];
    $scope.add = [
        { id: 1, name: "Field" },
        { id: 2, name: "Fiels Offices, HeadQuater"},
        { id: 3, name: "Fiels Offices, HeadQuater, Admin Created" },
        { id: 4, name: "Fiels Offices, HeadQuater, Admin Created,User Created" }
    ];
    $scope.appr = [
        { id: 0, name: "No Approvers" },
        { id: 1, name: "Single Approver" },
        { id: 2, name: "Multiple Approvers" }
    ];
    $scope.use = [
        { id: true, name: "Use Item Groups" },
        { id: false, name: "Don't Use Item Groups" }
    ];
    $scope.useI = [
        { id: false, name: "Don't Use Item Groups Seperated Freight" },
        { id: true, name: "Use Item Groups Seperated Freight" }
    ];
    $scope.Req = [
        { id: false, name: "Don't Present New Product Option" },
        { id: true, name: "Present New Product Option" }
    ];

    var formData = {};

    $scope.GetCustomersList = function () {
     
        NewRequestFactory.GetCustomers(3)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                    
                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.GetCustomersList();
    
    $scope.GetCustomerData = function (custID)
    {
        
        CustomerSettingsFactory.GetCustData(custID)
            .then(function (response) {
                
                if (response.data.length != 0) {
                    debugger;
                    $scope.form.CustomerId = parseInt(custID);
                    $scope.form.refx = response.data[0][0].Reference;
                    $scope.form.addx = response.data[0][0].Addresses;
                    $scope.form.usex = response.data[0][0].UserItem;
                    $scope.form.useIx = response.data[0][0].UserItemSp;
                    $scope.form.Reqx = response.data[0][0].RequestNew;
                    //////// checking condition ////////
                    var Approver = response.data[0][0].Approver;
                    if (Approver == 2)
                    {
                        $scope.form.apprxMul = response.data[0][0].No_Approver;
                        $scope.form.apprx = Approver; 
                    }
                    else
                    {
                        $scope.form.apprx = Approver; 
                    }
                }
            });
    }
    var bool = true;
    function validation()
    {
        bool = true;
        if ($scope.form.apprx == 2)
        {
            if ($scope.form.apprxMul == '' && $scope.form.apprxMul == undefined && $scope.form.apprxMul == 'undefined')
            {
                alert('Please Enter Number of Approvers');
                bool = false;
            }
        }
    }
    $scope.GetCustomerData(CustomerID);
    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }
    $scope.submitForm = function () {

        if (bool) {
            formData = $scope.form;
            var val = $scope.form.apprx;
            if (val != 2) {
                $scope.form.apprxMul = 0;
            }
            //alert(JSON.stringify(formData));
            var Data = {
                CustomerId: $scope.form.CustomerId,
                Reference: $scope.form.refx,
                Addresses: $scope.form.addx,
                Approver: $scope.form.apprx,
                UserItem: $scope.form.usex,
                UserItemSp: $scope.form.useIx,
                RequestNew: $scope.form.Reqx,
                No_Approver: $scope.form.apprxMul
            };
            // alert(JSON.stringify(Data));
            //console.log(Data);

            CustomerSettingsFactory.savePostData(Data)
                .then(function (response) {
                    // alert(response.data.length);
                    if (response.data.length != 0) {
                        alert('Request has been saved successfully.');
                    }
                });
        }
    };
       
 }]);