HomeApp.controller('RequestShippingController', ['$scope', '$window', 'AddFreightQuoteFactory', 'ViewRequestFactory', 'RequestShippingFactory', function ($scope, $window, AddFreightQuoteFactory, ViewRequestFactory, RequestShippingFactory) {
    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);

    $scope.showFlag = true;
    $scope.HideFlag = false;

    $scope.DocumentGroupId = 0;
    $scope.RequestDetails;
    $scope.DataCartItemsSave;
    $scope.ViewRequestOrderDetails = function () {
        //debugger;
        AddFreightQuoteFactory.ViewFreightQuoteDetails($scope.OrderID)
            .then(function (response) {
                //debugger;
                if (response.data[0][0].length != 0) {
                    $scope.RequestDetails = response.data[0][0];
                    $scope.form.SendEmailID = $scope.RequestDetails[0].UserName;
                }
                if (response.data[0][1].length != 0) {
                    $scope.ApprovarDetails = response.data[0][1];
                }
                if (response.data[0][2].length != 0) {
                    //debugger
                    $scope.CartItems = response.data[0][2];
                    $scope.DataCartItemsSave = response.data[0][2];
                }
                if (response.data[0][3].length != 0) {
                    $scope.SAdd1 = response.data[0][3][0];
                    $scope.SAdd2 = response.data[0][3][1];
                    $scope.SAdd3 = response.data[0][3][2];
                    $scope.SCity = response.data[0][3][3];
                    $scope.SState = response.data[0][3][4];
                    $scope.SZip = response.data[0][3][5];
                    $scope.SCountry = response.data[0][3][6];
                }
                if (response.data[0][4].length != 0) {
                    $scope.BAdd1 = response.data[0][4][0];
                    $scope.BAdd2 = response.data[0][4][1];
                    $scope.BAdd3 = response.data[0][4][2];
                    $scope.BCity = response.data[0][4][3];
                    $scope.BState = response.data[0][4][4];
                    $scope.BZip = response.data[0][4][5];
                    $scope.BCountry = response.data[0][4][6];
                }
                if (response.data[0][5].length != 0) {
                    $scope.Prodcount = response.data[0][5][0].ProdCount;
                }


                $scope.NewShipment();


            });
    }
    $scope.ViewRequestOrderDetails();


    $scope.BackToPrevious = function () {
        //debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


    $scope.submitForm = function (args1,args2) {
        debugger
        //OrderShipment
        var Data = {
            Title: $scope.form.Title,
            ShipmentDate: $scope.form.ShipmentDate,
            OrderId: $scope.OrderID,
            strOrderID: $scope.OrderID,
            isActive: 1,
            Type: 1,
            DocumentGroupId: $scope.DocumentGroupId,
            ShipmentId:0,
            SendEmail: $scope.form.SendEmail,
            SendEmailID: $scope.form.SendEmailID,
            ShipmentDetails: JSON.stringify(args1),//OrderShipment Details
        
            Ordershipmentinfo: JSON.stringify(args2),//Ordershipmentinfo
            //UserId: 1,
        }; 
        //alert(JSON.stringify(Data));

        RequestShippingFactory.AddOrdershipmentinfo(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    alert('Request has been saved successfully.');

                }
                $scope.getShipmentDataByOrderId();
            });
    }

    //update form all
    $scope.UpdateForm = function (args1, args2) {

        var Data = {
            Title: $scope.form.Title,
            ShipmentDate: $scope.form.ShipmentDate,
            OrderId: $scope.OrderID,
            strOrderID: $scope.OrderID,
            isActive: 1,
            Type: 2,
            DocumentGroupId: $scope.DocumentGroupId,
            ShipmentId: $scope.shipID,//using for update


            ShipmentDetails: JSON.stringify(args1),//OrderShipment Details

            Ordershipmentinfo: JSON.stringify(args2),//Ordershipmentinfo
            //UserId: 1,
        };
        //alert(JSON.stringify(Data));

        RequestShippingFactory.AddOrdershipmentinfo(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger
                    alert('Request has been saved successfully.');

                }
                $scope.getShipmentDataByOrderId();
            });

    }





    $scope.SaveAndNotify = function (Freight) {
        // debugger;
        var total = $scope.RequestDetails[0].TotalOrderAmount;
        var Freightlist =
        {

            Freightdata: JSON.stringify(Freight),
            TotalAmount: total,
            strOrderID: $scope.OrderID

        };


        // var  FreightlistDetails: 
        AddFreightQuoteFactory.AddFreightQuoteDetails(Freightlist)
            .then(function (response) {
                if (response.data.length != 0) {
                    $scope.RequestDetails[0].TotalOrderAmount = response.data[1];
                    alert('Request has been saved successfully.');

                }

            });
    }
    // select carrier
    $scope.GetSelectCarrierData = function () {
        //debugger;
        RequestShippingFactory.GetCarrierData()
            .then(function (response) {
                //debugger;
                    $scope.Carrier = response.data[0];
            });
        $scope.OrderShipmentInfo = [{ Waybill:0}];
    }
   
    $scope.GetSelectCarrierData();



    $scope.addRow = function () {
        //debugger
        var obj = {};
       
        obj.Waybill = '';
      
        $scope.OrderShipmentInfo.push(obj);
    };


    $scope.removeRow = function (value) {
        //alert(value);
        
        $scope.OrderShipmentInfo.splice(value, 1)
    };

  


    //New Upload File Code

    //Variables
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = "";
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = "";
    $scope.IsFileValid = "";
    $scope.IsFormValid = "";

    $scope.files = [];

    //File Select Event
    $scope.SelectFileforUpload = function (file) {
        //debugger
        //for single file
        $scope.SelectedFileForUpload = file[0];
        //for multiple files
        // STORE THE FILE OBJEC T IN AN ARRAY.
        for (var i = 0; i < file.length; i++) {
            $scope.files.push(file[i])
        }
    }





    $scope.uploadfiles = [];


    //Save uploadfiles
    $scope.SaveFile = function () {

        //$scope.FileDescription = 'Hello Testing...';
        //FILL FormData WITH FILE DETAILS.
        var data = new FormData();
        debugger

        for (var i = 0; i < $scope.files.length; i++) {
            data.append("files[" + i + "]", $scope.files[i])
            data.append("description[" + i + "]", $scope.uploadfiles[i].Description);
            data.append("OrderID[" + i + "]", $scope.OrderID);
            data.append("DocumentGroupID[" + i + "]", $scope.DocumentGroupId);
        }

        //    console.log(data);

        debugger

        ViewRequestFactory.UploadFile(data)
            .then(function (response) {
                $scope.OrderUploadedFiles = response.data[0];/// make a change response.data[0];
                $scope.DocumentGroupId = response.data[1];
                debugger
                $scope.UploadedFiles = [];
                for (var i = 0; i < $scope.OrderUploadedFiles.length; i++) {
                    if ($scope.OrderUploadedFiles[i].DocumentGroupId == $scope.DocumentGroupId) {
                        var obj = {};
                        obj.FileLocation = $scope.OrderUploadedFiles[i].FileLocation;
                        obj.FileName = $scope.OrderUploadedFiles[i].FileName;
                        obj.Description = $scope.OrderUploadedFiles[i].Description;
                       



                        $scope.UploadedFiles.push(obj);
                    }

                }
                alert('File uploaded successfully...');
                // $window.location.reload();
                $scope.files = [];
                $scope.uploadfiles = [];
                $scope.Add();
            });
    }


    $scope.getRequestedDocs = function () {

        ViewRequestFactory.GetRequestedDocs($scope.OrderID)
            .then(function (response) {
                //   console.log(response.data[0])
                //debugger
                $scope.OrderUploadedFiles = response.data[0];
               // $scope.filterUploadedFiles = $scope.OrderUploadedFiles.find(v => v.DocumentGroupId == $scope.DocumentGroupId);
                //$scope.UploadedFiles = $scope.filterUploadedFiles;
                //debugger
                $scope.UploadedFiles = [];
                for (var i = 0; i < $scope.OrderUploadedFiles.length; i++) {
                    if ($scope.OrderUploadedFiles[i].DocumentGroupId == $scope.DocumentGroupId) {
                        var obj = {};
                        obj.FileLocation = $scope.OrderUploadedFiles[i].FileLocation;
                        obj.FileName = $scope.OrderUploadedFiles[i].FileName;
                        obj.Description = $scope.OrderUploadedFiles[i].Description;
                        //obj.IsCat = false;



                        $scope.UploadedFiles.push(obj);
                    }
                    
                }
                //$scope.UploadedFiles = response.data[0];
            });
    }
    


    //Dynamic File Upload


    $scope.Add = function () {
        //Add the new item to the Array.
        var fileObj = {};
        fileObj.ImageUpload = $scope.ImageUpload;
        fileObj.Description = '';
        $scope.uploadfiles.push(fileObj);

        //Clear the TextBoxes.
        $scope.ImageUpload = "";
    };
    $scope.Add();

    $scope.Remove = function (index) {
        //Find the record using Index from Array.
        var name = $scope.uploadfiles[index].ImageUpload;
        if ($window.confirm("Do you want to delete: " + name)) {
            //Remove the item from Array using Index.
            $scope.uploadfiles.splice(index, 1);
        }
    }





    //get shipment data by order Id
    $scope.getShipmentDataByOrderId = function () {

            RequestShippingFactory.GetShipmentData($scope.OrderID)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger

                    $scope.shipData = response.data[0];

                    if ($scope.shipData.length > 0) {
                        $scope.splitship = true;
                    }

                } 

            });

    }
    $scope.getShipmentDataByOrderId();




    //get shipment data by click btn all details fill //args is  shipment id

    $scope.getOderShipment = function (args) {
        //alert(args);
        $scope.shipID = args;

        $scope.HideFlag = true;
        $scope.showFlag = false;

        RequestShippingFactory.GetShipmentAll(args)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger
                    $scope.CartItemsSave = response.data[0];
                    $scope.OrderShipmentInfo = response.data[1];
                    //debugger
                    for (var j = 0; j < $scope.OrderShipmentInfo.length; j++) {
                        if ($scope.OrderShipmentInfo[j].DeliveryDate != null) {
                            var dateOut = new Date($scope.OrderShipmentInfo[j].DeliveryDate);
                            $scope.OrderShipmentInfo[j].DeliveryDate = dateOut;
                        } else {
                            $scope.OrderShipmentInfo[j].DeliveryDate = null;
                        }
                          
                    }
                    $scope.form.Title = $scope.shipData.find(v => v.ShipmentId == args).Title;

                    var dateOutship = new Date($scope.shipData.find(v => v.ShipmentId == args).ShipmentDate);
                    $scope.form.ShipmentDate = dateOutship;


                    $scope.DocumentGroupId = $scope.shipData.find(v => v.ShipmentId == args).DocumentGroupId;

                    $scope.getRequestedDocs();

                }
            });
    }

    $scope.NewShipment = function () {

        $scope.OrderShipmentInfo = [];
        $scope.OrderShipmentInfo = [{ Waybill: 0 }];
        $scope.CartItemsSave = [];
        $scope.form.Title = '';
        $scope.form.ShipmentDate = null;
        $scope.NewShip = false;
        //alert($scope.OrderID);
        RequestShippingFactory.GetNewShipmentData($scope.OrderID)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    if (response.data[0].length == 0)
                    {
                        $scope.CartItemsSave = $scope.DataCartItemsSave;
                    }
                    else
                    {
                        $scope.CartItemsSave = response.data[0];
                        $scope.NewShip = true;
                    }
                    

                } else {
                    alert("Something Went Wrong");
                }

            });
    }

    $scope.GetBalance = function (index,Qty,Toship,BalQty) {
        debugger;
        var tempBal = BalQty;
        if ($scope.NewShip) {
            if (BalQty >= Toship) {
                var bal = parseInt(BalQty) - parseInt(Toship);
                $scope.CartItemsSave[index].BalanceQty = bal;
            } else
            {
                alert("Please Enter Correct ToShip, Check Balance Shipment");
                $scope.CartItemsSave[index].BalanceQty = tempBal; // '';
                $scope.CartItemsSave[index].ToShip = '';
                $scope.NewShipment();
            }
        }
        else {
            if (Qty >= Toship)
            {
                var bal = parseInt(Qty) - parseInt(Toship);
                $scope.CartItemsSave[index].BalanceQty = bal;
            } else {
                alert("Please Enter Correct ToShip, Check Balance Shipment");
                $scope.CartItemsSave[index].BalanceQty = tempBal; //'';
                $scope.CartItemsSave[index].ToShip = '';
            }
        }

    }

    $scope.AutofillShipment = function (chk) {
        // alert(chk);
        debugger;
        if (chk) {
            //$scope.ViewRequestOrderDetails();
            $scope.getShipmentDataByOrderId();
            $scope.GetSelectCarrierData();

            $scope.CartItemsSaveFiles = $scope.CartItemsSave;

            for (var j = 0; j < $scope.CartItemsSave.length; j++)
            {

                $scope.CartItemsSave[j].ToShip = '';
                $scope.CartItemsSave[j].BalanceQty = $scope.CartItemsSave[j].Quantity;

            }
           // window.location.reload();
        }
      
        else {
            $scope.CartItemsSaveFiles = $scope.CartItemsSave;

            for (var j = 0; j < $scope.CartItemsSave.length; j++) {

                $scope.CartItemsSave[j].ToShip = $scope.CartItemsSave[j].Quantity;
                $scope.CartItemsSave[j].BalanceQty = 0;

            }
        }
        

    }

}]);