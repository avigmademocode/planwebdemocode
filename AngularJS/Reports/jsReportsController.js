HomeApp.controller('ReportsController', ['$scope', '$window', 'NewRequestFactory', 'ReportsFactory', function ($scope, $window, NewRequestFactory, ReportsFactory) {

    $scope.hideNshow = function () {
        $scope.TableShow = false;
        $scope.DetailsTableShow = false;
        $scope.hideChekcbox = true;
    }
    $scope.hideNshow();

    $scope.month = [
        { id: 1, name: "January" },
        { id: 2, name: "February" },
        { id: 3, name: "March" },
        { id: 4, name: "April" },
        { id: 5, name: "May" },
        { id: 6, name: "June" },
        { id: 7, name: "July" },
        { id: 8, name: "August" },
        { id: 9, name: "September" },
        { id: 10, name: "October" },
        { id: 11, name: "November" },
        { id: 12, name: "December" }
    ];


    $scope.year = [
        { id: 2015, name: "2015" },
        { id: 2016, name: "2016" },
        { id: 2017, name: "2017" },
        { id: 2018, name: "2018" }
    ];

    $scope.ChStartmonth = function (Start) {
        //debugger
        if ($scope.form.Endmonth == null) {
            $scope.form.Endmonth == undefined;
        } else
            if ($scope.form.Startmonth > $scope.form.Endmonth) {
                $scope.form.Startmonth = null;
                $scope.form.Endmonth = null;
                alert("Incorrect Month");
            }
        $scope.hideNshow();
    }

    $scope.ChEndmonth = function (End) {
        //debugger
        if ($scope.form.Startmonth > $scope.form.Endmonth) {
            $scope.form.Endmonth = null;
            alert("Incorrect Month");
        }
        $scope.hideNshow();
    }
    $scope.ChStartYear = function (Start) {
        //debugger
        if ($scope.form.Endyear == null) {
            $scope.form.Endyear == undefined;
        } else
            if ($scope.form.Startyear > $scope.form.Endyear) {
                $scope.form.Startyear = null;
                $scope.form.Endyear = null;
                alert("Incorrect Year");
            }
        $scope.hideNshow();
    }


    $scope.checkErr = function (startDate, endDate) {
        debugger
        var curDate = new Date();
        if (new Date(startDate) > new Date(endDate)) {
            alert("End Date should be greater than start date..");
            //$scope.form.startDate = "";
            $scope.form.endDate = "";
            return false;

        }
        $scope.hideNshow();
    };  ///validation for date



    $scope.ChEndYear = function (End) {
        //debugger
        if ($scope.form.Startyear > $scope.form.Endyear) {
            $scope.form.Endyear = null;
            alert("Incorrect Year");
        }
        $scope.hideNshow();
    }


    $scope.submitForm = function () {
        //debugger
        var Data = {

            CustomerID: $scope.form.CustomerId,
            DateStart: $scope.form.startDate,
            DateEnd: $scope.form.endDate,

            StatusID: $scope.form.StatusId
        };
        //alert(JSON.stringify(Data));

        ReportsFactory.GetReports(Data)
            .then(function (response) {
                //debugger
                $scope.CounrtyByData = response.data[0];
                $scope.ReportsData = response.data[1];
                $scope.hideChekcbox = false;
                $scope.valCounrty = true;
                $scope.TableShow = true;

            });

    }




    $scope.GetCustomersList = function () {
        NewRequestFactory.GetCustomers(1)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger;
                    $scope.custList = response.data[0];
                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.GetCustomersList();


    $scope.GetStatusList = function () {
        ReportsFactory.GetCustomerStatus()
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger;
                    $scope.custStatusList = response.data[0];
                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.GetStatusList();

    $scope.GetCustStatus = function (CustID)
    {
        debugger;
        $scope.StatusList = [];
   
        for (var i = 0; i < $scope.custStatusList.length; i++) {
            if ($scope.custStatusList[i].CustId == CustID)
            {
                var obj = {};
                obj.StatusId = $scope.custStatusList[i].StatusId;
                obj.StatusName = $scope.custStatusList[i].StatusName;
               
                $scope.StatusList.push(obj);

            }
        }
            
    }

    $scope.checkval = function (val) {
        // alert(val);
        $scope.TableShow = true;
        $scope.valDetails = false;
        $scope.DetailsTableShow = false;
    }

    $scope.checkdetails = function (val) {
        // alert(val);
        $scope.DetailsTableShow = true;
        $scope.valCounrty = false;
        $scope.TableShow = false;


    }







}]);