HomeApp.controller('RequestOrderController', ['$scope', '$window', 'RequestOrderFactory', function ($scope, $window, RequestOrderFactory) {
    debugger;
   // $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
   // $scope.ApproverID = $scope.ApproverID ? $scope.ApproverID.split('?')[1] : window.location.search.slice(1);
    $scope.RequestDetails;
    $scope.OrderID = GetQueryStringParams('Oval');
    $scope.ApproverID = GetQueryStringParams('AppId');

    function GetQueryStringParams(sParam)
    {

        var sPageURL = window.location.search.substring(1);

        var sURLVariables = sPageURL.split('&');

        for (var i = 0; i < sURLVariables.length; i++) {

            var sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] == sParam) {

                return sParameterName[1];

            }

        }
    }


        $scope.ViewRequestOrderDetails = function () {
            
        var data =
            {
                strorderID: $scope.OrderID
        };
        RequestOrderFactory.ViewRequestOrderDetails(data)
            .then(function (response) {


                if (response.data[0].length != 0) {
                    $scope.RequestDetails = response.data[0];
                    $scope.ReferenceNo = response.data[0][0].ReferenceNo;

                    $scope.CustomerId = response.data[0][0].CustID;
                }
                if (response.data[1].length != 0) {
                    $scope.ApprovarDetails = response.data[1];
                }
                if (response.data[2].length != 0) {
                    $scope.CartItems = response.data[2];
                }
                if (response.data[3].length != 0) {
                    $scope.SAdd1 = response.data[3][0];
                    $scope.SAdd2 = response.data[3][1];
                    $scope.SAdd3 = response.data[3][2];
                    $scope.SCity = response.data[3][3];
                    $scope.SState = response.data[3][4];
                    $scope.SZip = response.data[3][5];
                    $scope.SCountry = response.data[3][6];
                }
                if (response.data[4].length != 0) {
                    $scope.BAdd1 = response.data[4][0];
                    $scope.BAdd2 = response.data[4][1];
                    $scope.BAdd3 = response.data[4][2];
                    $scope.BCity = response.data[4][3];
                    $scope.BState = response.data[4][4];
                    $scope.BZip = response.data[4][5];
                    $scope.BCountry = response.data[4][6];
                }
                if (response.data[5].length != 0) {
                    $scope.Prodcount = response.data[5][0].ProdCount;
                }
                if (response.data[6].length != 0) {
                    $scope.Status = response.data[6];
                }
                if (response.data[7] != null) {
                    if (response.data[7].length != 0) {
                        $scope.OrderLog = response.data[7];
                    }
                }

                if (response.data[8].length != 0) {

                    $scope.Buttonlist = response.data[8];
                }

                if (response.data[9].length != 0) {

                    $scope.IncoTermlist = response.data[9];
                    // console.log(response.data[9]);
                }

                if (response.data[10].length != 0) {

                    $scope.Contactlist = response.data[10];
                    //  console.log(response.data[10]);
                }


            });
    }
    $scope.ViewRequestOrderDetails();

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


    


    $scope.Denny = function () {

         debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 5,
            ChangedStatus: 9,  // For Cancel Last StatusID = -1
            Reason: $scope.LeadReason, // Resason Value for that Pop up
            //LeadTime: $scope.LeadTime, // Pass the textbox value 
            IncoID: null,
            FullStatus: null,
            ApproverID: $scope.ApproverID
        }
        RequestOrderFactory.ChangedStatus(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }
            })
        $scope.ViewRequestOrderDetails();
        //   $window.location.reload();
      //  $scope.EditCustomer();
    }


    $scope.Approve = function () {

          debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 6,
            ChangedStatus: 0,
            Reason: $scope.Reason,
            LeadTime: null,
            IncoID: null,
            FullStatus: null,
            ApproverID: $scope.ApproverID
        }
        RequestOrderFactory.ChangedStatus(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }
            })
        $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }


    //email popup
    $scope.SelectSub = function (arg) {
        if (arg) {
            $scope.form.Subject = "ReferenceNo:- " + $scope.ReferenceNo;
        }
        else {
            $scope.form.Subject = "";
        }
    }
    $scope.submitForm = function () {
        var Data = {
            IsBodyHtml: true,
            From: "",
            To: "",
            Subject: $scope.form.Subject,
            Message: $scope.form.Message,
            CustId: $scope.CustomerId,
            UserId: 0,
            strOrderID: $scope.OrderID,
            Type: 1
        };
        $scope.form.Subject = "";
        $scope.form.Message = "";
        $scope.form.MessageType = null;
        RequestOrderFactory.SendEmailData(Data)
            .then(function (response) {
                //alert(response.data);
                if (response.data.length != 0) {
                    alert('Request has been sent successfully.');
                }
            });
    };

}]);