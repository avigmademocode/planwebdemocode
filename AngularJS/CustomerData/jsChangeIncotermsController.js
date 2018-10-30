HomeApp.controller('ChangeIncotermsController', ['$scope', '$window', 'NewRequestFactory', 'ChangeIncotermsFactory', function ($scope, $window, NewRequestFactory, ChangeIncotermsFactory) {

    $scope.GetCustomersList = function (val) {
        debugger;
        NewRequestFactory.GetCustomers(val)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                }
            });
    }
    $scope.GetCustomersList(2);

    $scope.AssignedCustIncoTerms = {};
    $scope.IncoTermId;
    $scope.GetCustIncoTermData = function (IncoTermId) {
        debugger
        if (IncoTermId == '' || IncoTermId == "" || IncoTermId == null) {
            IncoTermId = 0;
        }
        ChangeIncotermsFactory.GetCustIncoTermDtls(IncoTermId)
            .then(function (response) {
                debugger
                if (response.data.length > 0) {
                    $scope.CustIncoTermData = response.data[0];
                    if (response.data.length > 1) {
                        
                        $scope.AssignedCustIncoTerms = response.data[1];
                    }
                }
            });
    }
    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }
    $scope.GetCustIncoTermData(0);

    //$scope.EditIncoterm = function (obj) {
    //    $scope.GetCustIncoTermData(obj.IncoTermId);
    //}

    $scope.addRow = function () {

        var obj = {};
        $scope.Edit = true;
        obj.IsActive = true;
        obj.ProdCatId = 0;
        obj.ProdCatDesc = '';
        obj.UserId = 0;
        obj.Type = 1;
        obj.IsEdit = $scope.Edit;
        obj.Ischange = 1;

        $scope.ProductCategories.push(obj);
    };
    $scope.UpdateIncoterms = function (IncoTermList) {
        debugger;
        var data =
            {
                IncoTermId: 0,
                IncoTermCode: "",
                IncoTermDesc: "",
                IncoTermData: JSON.stringify(IncoTermList),
                Type: 1
            }
        ChangeIncotermsFactory.SaveIncoTerm(data)
            .then(function (response) {
                if (response.data.length > 0) {
                    debugger;
                    if (response.data[0] == -99)
                    {
                        alert('Incoterm Already Exists.');
                    }
                    else if (response.data[0] == -99) {
                        alert('Incoterm Code Already Exists.');
                    }
                    else
                    {
                        alert('Request has been saved successfully.');
                    }
                    $scope.CustIncoTermData = response.data[1][0];
                    $scope.AssignedCustIncoTerms = response.data[1][1];
                    

                }
            });

    }

    $scope.SaveIncoTerm = function () {
        //console.log($scope.Customers)
        debugger;
        var bool = validation();
        if (bool) {


            var data =
                {
                    IncoTermId: $scope.IncoTermId,
                    IncoTermCode: $scope.IncoTermCode,
                    IncoTermDesc: $scope.IncoTermDesc,
                    CustID: JSON.stringify($scope.Finalcustlist),
                    Type: 2
                }
            ChangeIncotermsFactory.SaveIncoTerm(data)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        
                        $scope.CustIncoTermData = response.data[0][0];
                        $scope.AssignedCustIncoTerms = response.data[0][1];
                        if (response.data[1] == -99)
                        {
                            alert('IncoTerm Code  Already Exists.');
                        }
                        else if (response.data[1] == -98) {
                            alert('IncoTerm Desc Already Exists.');
                        }
                        else
                        {
                            alert('Request has been saved successfully.');
                        }

                    }
                });
        }
         
    }

    $scope.Checked = function () {
        for (var i = 0; i <= $scope.CustIncoTermData.length; i++)
        {
            $scope.CustIncoTermData[i].IsEdit = false;
            $scope.CustIncoTermData[i].Ischange = 0;
          
        }
    }
    var bool = true;
    function validation()
    {
        bool = true;
        if ($scope.IncoTermCode == '' || $scope.IncoTermCode == 'undefined' || $scope.IncoTermCode == undefined)
        {
            bool = false;
            alert('Please Enter IncoTerm Code');
            return bool;
        }
        if ($scope.IncoTermDesc == '' || $scope.IncoTermDesc == 'undefined' || $scope.IncoTermDesc == undefined) {
            bool = false;
            alert('Please Enter IncoTerm Description');
            return bool;
        }
        bool = false;
        for (var i = 0; i < $scope.Finalcustlist.length; i++)
        {
            if ($scope.Finalcustlist[i].IsCat == true)
            {
                bool = true;
                return bool;
            }
            else
            {
                bool = false;
            }
           
        }
        if (!bool) {
            alert('Please Select Atleast one Customer');
            return bool;
        }
    }
    $scope.EditIncoterm = function (IncoTermData) {
        debugger;
        

        $scope.tempcustlist = [];
        $scope.Finalcustlist = [];
        $scope.IncoTermCode = IncoTermData.IncoTermCode;
        $scope.IncoTermDesc = IncoTermData.IncoTermDesc;
        $scope.IncoTermId = IncoTermData.IncoTermId

        $scope.CustID = $scope.CustomerId; //=> This is dropdown value => Replace with CatData.CustId (Getting null value)

        for (var i = 0; i < $scope.AssignedCustIncoTerms.length; i++) {
            if ($scope.AssignedCustIncoTerms[i].IncoTermId == IncoTermData.IncoTermId) {
               
                    var obj = {};
                   obj.CustId = $scope.AssignedCustIncoTerms[i].CustId;
                    obj.IsCat = true;
                    $scope.tempcustlist.push(obj);
                

            }

        }
        debugger

        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            for (var j = 0; j < $scope.tempcustlist.length; j++) {
                if ($scope.custList[i].CustId == $scope.tempcustlist[j].CustId) {
                    obj.IsCat = true;
                }

            }

            $scope.Finalcustlist.push(obj);
        }

    }

    $scope.ResetData = function () {
        debugger;
        $scope.IncoTermCode = '';
        $scope.IncoTermDesc = '';
        $scope.IncoTermId = 0;
        $scope.Finalcustlist = [];
        for (var i = 0; i < $scope.custList.length; i++) {
            var obj = {};
            obj.CustId = $scope.custList[i].CustId;
            obj.CustName = $scope.custList[i].CustName;
            obj.IsCat = false;
            $scope.Finalcustlist.push(obj);
        }
    }


}]);