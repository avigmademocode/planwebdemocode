HomeApp.controller('AddEditCustomerStatusController', ['$scope', '$window', 'NewRequestFactory', 'AddEditCustomerStatusFactory', function ($scope, $window, NewRequestFactory, AddEditCustomerStatusFactory) {

    var CustomerID = $window.localStorage.getItem('MCustID');
   // CustomerID = 1;
    $scope.SortCustStatusList ;
    $scope.GetCustomersList = function () {
       /// debugger;
        NewRequestFactory.GetCustomers(3)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.custList = response.data[0];
                }
            });
    }
   

    $scope.GetCustStatus = function (CustId) {
      //  debugger;
        if (CustId != null)
        {
            $scope.CustomerId = parseInt(CustId);
        }
        else {
            $scope.CustomerId = parseInt(0);
        }
        AddEditCustomerStatusFactory.GetCustStatusData(CustId)
            .then(function (response) {
                if (response.data.length != 0) {
                    
                    $scope.CustStatusList = response.data[0];
                    $scope.FinalCustStatusList = response.data[0];
                }
            });
    }

    $scope.GetCustomersList();
   // $scope.GetCustStatus(CustomerID);
    $scope.GetCustStatus(CustomerID);


    var bool = true;
    function validation() {
        bool = true;
        if ($scope.StatusName == '' || $scope.StatusName == 'undefined' || $scope.StatusName == undefined) {
            bool = false;
            alert('Please Enter Status Name');
            return bool;
        }
        if ($scope.AltName == '' || $scope.AltName == 'undefined' || $scope.AltName == undefined) {
            bool = false;
            alert('Please Enter Alt Name');
            return bool;
        }
        return bool;
       
    }



    $scope.SaveNewStatus = function () {
        debugger;
        var bool = validation();
        if (bool) {
            // debugger;
            var StatusData = {
                StatusName: $scope.StatusName,
                AltName: $scope.AltName,
                UserAction: $scope.UserAction,
                CustId: $scope.CustomerId,
                ShowStatus : 1,
                Type: 1
            }
            AddEditCustomerStatusFactory.SaveStatusData(StatusData)
                .then(function (response) {
                    if (response.data.length > 0) {
                        debugger;
                        if (response.data[0] == -99)
                        {
                            alert('Status Name Already Exists.');
                        }
                      else  if (response.data[0] == -98) {
                            alert('Alt Name Already Exists.');
                        }
                        else {
                            alert('Request has been saved successfully.');
                        }
                        $scope.CustStatusList = response.data[1][0];
                        
                        ResetData();
                    }
                });
        }
      
    }
    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }

    function ResetData()
    {
        $scope.StatusName = '';
        $scope.UserAction = false;
        $scope.AltName = '';
    }

    $scope.SaveStatus = function (CustStatusList)
    {
        //debugger;
        var data =
            {
                CustStatusData: JSON.stringify(CustStatusList),
                CustId: $scope.CustomerId,
                Type:2
            }
        AddEditCustomerStatusFactory.SaveStatusData(data)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger;
                    $scope.CustStatusList = response.data[1][0];
                    alert('Request has been saved successfully.');
                }
            });
    }

     

    function SortList(oldindex, newindex) {
        //debugger;
 
        var oldIndex = parseInt(oldindex);
        var arrayClone = null;
        angular.forEach($scope.CustStatusList, function (item, index) {
            if (index == oldindex) {
                if (oldIndex > -1) {
                   
                    var newIndex = newindex;

                    if (newIndex < 0) {
                        newIndex = 0
                    } else if (newIndex >= $scope.CustStatusList.length) {
                        newIndex = $scope.CustStatusList.length
                    }
                 
                    arrayClone = $scope.CustStatusList.slice();
                    arrayClone.splice(index, 1);
                    arrayClone.splice(newIndex, 0, item);
                 
                   // console.log(arrayClone)
                }
            }
        })
     //   $scope.CustStatusList.find(v => v.GrantBudgetId == GrantBudgetId).Serial = orgserial;
        $scope.CustStatusList = arrayClone;

        for (var i = 0; i < $scope.CustStatusList.length; i++)
        {
            var statusID = $scope.CustStatusList[i].StatusId;
            $scope.CustStatusList.find(v => v.StatusId == statusID).StatusOrder = i+1;
        }







        //alert(newindex)
        //alert(oldindex)

        //for (var x in $scope.custList) $scope.custList[x].CustId == oldindex ? $scope.custList.push($scope.custList.splice(x, 1)[0]) : 0;

        //console.log($scope.custList);
        //var val = $scope.FinalCustStatusList.findIndex(oldindex);
        //  var val = $filter('filter')($scope.FinalCustStatusList, { id: parseInt($stateParams.friendId) }, true)[0];
        // alert(val);
    }

    $('#sortable').sortable({
        start: function (e, ui) {
            // creates a temporary attribute on the element with the old index
            $(this).attr('data-previndex', ui.item.index());
        },
        update: function (e, ui) {
            // gets the new and old index then removes the temporary attribute
            var newIndex = ui.item.index();
            var oldIndex = $(this).attr('data-previndex');
            SortList(oldIndex, newIndex);
            $(this).removeAttr('data-previndex');
        }
    });
}]);