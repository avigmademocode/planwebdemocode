HomeApp.controller('ViewRequestController', ['$scope', '$window', '$location', 'ViewRequestFactory', function ($scope, $window, $location, ViewRequestFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(9);
    $scope.CustomerId;
    $scope.btnAdd = false;
    $scope.btnEdit = true;
    $scope.uploadfiles = [];
    $scope.ViewGrantbtnHide = true;
    $scope.HideShowItSetUp = true;
    $scope.HideShowSoftwareSetup = true;

    $scope.GrantCodeOrderDetails = function () {
        ViewRequestFactory.GrantCodeOrderDetails($scope.OrderID)
            .then(function (response) {
                if (response.data[0].length > 0) {
                    //$scope.CalcTotalAmt(response.data[1]);
                    debugger
                    $scope.Seriallst = response.data[1];
                    $scope.grantcodeOrderdatalst = response.data[0];
                    $scope.OriginalTotalOrderAmount = response.data[2][0].TotalOrderAmount;
                    $scope.ViewGrantbtnHide = false;
                }
                else
                {
                    $scope.ViewGrantbtnHide = true;
                }

            });
    }
    // $scope.GrantCodeOrderDetails();

    $scope.BudgetCodeOrderDetails = function () {
        ViewRequestFactory.BudgetCodeOrderDetails($scope.OrderID)
            .then(function (response) {
                if (response.data[0].length > 0) {
                    //$scope.CalcTotalAmt(response.data[1]);
                    debugger
                    $scope.Seriallst = response.data[1];
                    $scope.grantcodeOrderdatalst = response.data[0];
                    $scope.OriginalTotalOrderAmount = response.data[2][0].TotalOrderAmount;
                    $scope.ViewGrantbtnHide = false;
                }
                else
                {
                    $scope.ViewGrantbtnHide = true;
                }
            });
    }

    $scope.ViewRequestOrderDetails = function () {
          //debugger;
        ViewRequestFactory.viewRequestOrderDetails($scope.OrderID)
            .then(function (response) {

                if ($window.localStorage.getItem('Editbtn') != null)
                {
                    var BtnID = JSON.parse($window.localStorage.getItem('Editbtn'));
                    if (BtnID == '1')
                    {
                        $scope.btnEdit = false;
                        $scope.btnAdd = true;
                    }
                    else
                    {
                        $scope.btnEdit = true;
                        $scope.btnAdd = false;
                    }
                }


                if (response.data[0].length != 0) {
                    $scope.RequestDetails = response.data[0];
                    $scope.ReferenceNo = response.data[0][0].ReferenceNo;
                    $scope.StatusID = response.data[0][0].StatusID;
                    $scope.CustomerId = response.data[0][0].CustID;
                    $scope.Feight = response.data[0][0].Feight;
                    $scope.TaxAmount = response.data[0][0].TaxValue;
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
                debugger;
                if (response.data[12] != 'undefined' && response.data[12] != undefined)
                {
                    if (response.data[12].length != 0) {

                        $scope.PlanComment = response.data[12];
                         
                    }
                }
                
                if (response.data[13].length != 0) {

                    $scope.CustSettingList = response.data[13];
                    $scope.BudgetGrantShowHide = $scope.CustSettingList[0].UseItemGroups;
                    // Grant Code
                    $scope.Granthide = false;
                    $scope.Budgethide = false;
                    if ($scope.BudgetGrantShowHide)
                    {
                        $scope.Granthide = false;
                        $scope.Budgethide = true;
                        $scope.GrantCodeOrderDetails();
                    }
                    else // Budget Code
                    {
                        $scope.Granthide = true;
                        $scope.Budgethide = false;
                        $scope.BudgetCodeOrderDetails();
                    }
                    //  console.log(response.data[10]);
                }

                $scope.Feighthide = false;
                if ($scope.Feight == 0) {
                    $scope.Feighthide = true;
                }
                else {
                    $scope.Feighthide = false;
                }
                $scope.Taxhide = false;
                if ($scope.TaxAmount == 0) {
                    $scope.Taxhide = true;
                }
                else
                {
                    $scope.Taxhide = false;
                }

                if (response.data[14] != null)
                {
                    if (response.data[14].length != 0)
                    {

                        $scope.FeightList = response.data[14];
                        $scope.Taxval = $scope.FeightList.find(v => v.isTaxApplicable == true).Value;
                        //  console.log(response.data[10]);
                    }
                }

                debugger;
                if (response.data[15] != null) {
                    if (response.data[15].length != 0) {

                        $scope.ItSetUpList = response.data[15];
                        for (var i = 0; i < $scope.ItSetUpList.length; i++) {
                            if ($scope.ItSetUpList[i].DataIt.length > 0) {
                                $scope.HideShowItSetUp = false;
                                break;
                            }
                        }
              
                   
                        
                    }
                }

                if (response.data[16] != null) {
                    if (response.data[16].length != 0) {

                        $scope.SSsetUpList = response.data[16];
                        for (var i = 0; i < $scope.SSsetUpList.length; i++)
                        {
                            if ($scope.SSsetUpList[i].DataSS.length > 0)
                            {
                                $scope.HideShowSoftwareSetup = false;
                                break;
                            }
                        }
                       

                    }
                }

            });
    }
    $scope.ViewRequestOrderDetails();


  

    $scope.GetComments = function () {
        debugger;
        $scope.PlanComments = [];
        for (var i = 0; i < $scope.PlanComment.length; i++) {
            var obj = {};
            obj.Comments = $scope.PlanComment[i].Comments;
            $scope.PlanComments.push(obj);
        }
       
    }
    $scope.AddFreight = function () {
        $window.location.href = '/AddFreightQuote?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.AddBudget = function (val) {
        $window.localStorage.setItem("EditBudget", val);
        $window.location.href = '/RequestFinalize?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }


    $scope.AddGrantCode = function ()
    {
        $window.localStorage.setItem("Editbtn", "1");
        $window.location.href = '/AddNewGrantCode?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.AddITSetup = function (val) {
        
        $window.location.href = '/Itsetup?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }



    $scope.OnImageChange = function (picFile)
    {
        //   debuggerBu
        $scope.ImgFile = picFile;
    }

    //$scope.formData = [];
    $scope.saveDocuments = function () {
        //  debugger
        ViewRequestFactory.saveDocuments(picFile)
            .then(function (response) {

            })
    }

    //$scope.Print = function () {
    //    $window.print();
    //}

    //$scope.PrintExcel = function () {
    //    debugger;
    //    var Data = {
    //        strorderID: $scope.OrderID,
    //    }
    //    ViewRequestFactory.PrintExcel(Data)
    //        .then(function (response) {

    //        })
    //}

    $scope.EditCustomer = function () {
        //debugger;
        $window.location.href = '/NewRequest?' + encodeURIComponent(decodeURIComponent($scope.OrderID));

    }

    $scope.ShipDetails = function () {
        //debugger;
        $window.location.href = '/RequestShipping?' + encodeURIComponent(decodeURIComponent($scope.OrderID));

    }

    $scope.SelectProduct = function () {
        //debugger;
        $window.localStorage.removeItem('pageID');
        $window.localStorage.setItem('pageID', JSON.stringify('4'));
        $window.localStorage.removeItem('CustomerId');
        $window.localStorage.setItem('CustomerId', $scope.CustomerId);
        $window.location.href = '/Product?' + encodeURIComponent(decodeURIComponent($scope.OrderID));

    }


    $scope.OnHold = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 11,
            ChangedStatus: 16,  // For Cancel Last StatusID = -1
            Reason: $scope.HoldRemark, // Resason Value for that Pop up
            LeadTime: 0,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null,
            SendEmail: null,
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {

                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                    $scope.HoldRemark = '';
                }

            })
        $scope.ViewRequestOrderDetails();
        //  $window.location.reload();
    }



    $scope.EnterSON = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 10,
            ChangedStatus: 0,  // For Cancel Last StatusID = -1
            Reason: null, // Resason Value for that Pop up
            LeadTime: 0,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: $scope.SalesOrderNum,
            SendEmail: $scope.Email
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {
                debugger;
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                    $scope.SalesOrderNum = '';
                    $scope.Email = false;
                }

            })
        $scope.ViewRequestOrderDetails();
        //  $window.location.reload();
    }


    $scope.SaveStatusChange = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 1,
            ChangedStatus: $scope.ChangedStatus,  // For Cancel Pass StatusID = 0
            Reason: $scope.Reason,
            LeadTime: null,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
           .then(function (response) {
               if (response.data.length != 0)
               {
                   alert('Request has been saved successfully.');
               }
           })
        $scope.ViewRequestOrderDetails();
        //$window.location.reload();
    }


    $scope.CancelStatusChange = function () {

        //  debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 2,
            ChangedStatus: 0,  // For Cancel Pass StatusID = 0
            Reason: $scope.Reason, // Resason Value for that Pop up
            LeadTime: null,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo:null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {

                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }

            })
        $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }




    $scope.LastStatusChange = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 3,
            ChangedStatus: -1,  // For Cancel Last StatusID = -1
            Reason: $scope.Reason, // Resason Value for that Pop up
            LeadTime: 0,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null

        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {

                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }

            })
        $scope.ViewRequestOrderDetails();
        //  $window.location.reload();
    }

    $scope.ChangeStatustoOne = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 5,
            ChangedStatus: 1,  // For Cancel Last StatusID = -1
            Reason:null, // Resason Value for that Pop up
            //LeadTime: $scope.LeadTime, // Pass the textbox value 
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {

                if (response.data.length != 0)
                {
                    alert('Request has been saved successfully.');
                    $scope.EditCustomer();
                }

            })
        //$scope.ViewRequestOrderDetails();
        //   $window.location.reload();
        $scope.EditCustomer();
    }

    $scope.Denny = function () {

        debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 5,
            ChangedStatus: 9,  // For Cancel Last StatusID = -1
            Reason: null, // Resason Value for that Pop up
            //LeadTime: $scope.LeadTime, // Pass the textbox value 
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {
                debugger;
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }
            })
        //$scope.ViewRequestOrderDetails();
        //   $window.location.reload();
        $scope.ViewRequestOrderDetails();
    }



    $scope.FreightChange = function () {

        // debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 4,
            ChangedStatus: null,  // For Cancel Last StatusID = -1
            Reason: $scope.LeadReason, // Resason Value for that Pop up
            //LeadTime: $scope.LeadTime, // Pass the textbox value 
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }
            })
        $scope.ViewRequestOrderDetails();
        //   $window.location.reload();
    }

         $scope.Approve  = function () {

        //  debugger;
             var Data = {

            OrderID: $scope.OrderID,
            Type: 6,
            ChangedStatus: $scope.StatusID,   
            Reason: null,  
            LeadTime: null,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {
                debugger;
                if (response.data.length != 0) {
                    debugger;
                    alert('Request has been saved successfully.');
                }
            })
        $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }


    $scope.FreightForwarder  = function () {

        //  debugger;
        var Data = {
            OrderID: $scope.OrderID,
            Type: 7,
            ChangedStatus: $scope.StatusID,
            Reason: null,
            LeadTime: null,
            IncoID: null,
            FullStatus: null,
            SalesOrderNo: null
        }
        ViewRequestFactory.ChangedStatus(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');
                }
            })
        $scope.ViewRequestOrderDetails();
        // $window.location.reload();
    }


    $scope.SaveDifferentIncoterm = function () {
        debugger
        alert($scope.Incoterm)
        if ($scope.Incoterm != '' && $scope.Incoterm != 'undefined' && $scope.Incoterm != null) {

            //  debugger;
            var Data = {
                OrderID: $scope.OrderID,
                Type: 9,
                ChangedStatus: $scope.StatusID,
                Reason: null,
                LeadTime: null,
                IncoID: $scope.Incoterm,// Need to Fetch from UI
                FullStatus: null,
                SalesOrderNo: null
            }
            ViewRequestFactory.ChangedStatus(Data)
                .then(function (response) {
                    if (response.data.length != 0) {
                        alert('Request has been saved successfully.');
                    }
                })
            $scope.ViewRequestOrderDetails();

            // $window.location.reload();
        }
        else {
            alert('Please Select Incoterm!');
        }
    }

    $("#incoterm").show();
    $("#incoterms").hide();

    $scope.HideShowIncoterm = function () {
        $("#incoterm").hide();
        $("#incoterms").show();
    }


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
        }

    //    console.log(data);

        debugger

        ViewRequestFactory.UploadFile(data)
            .then(function (response) {
                $scope.UploadedFiles = response.data[0];
                alert('File uploaded successfully...');
                // $window.location.reload();
                $scope.files = [];
                $scope.uploadfiles = [];
                $scope.Add();
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

    function download() {
        var iframe = document.getElementById('invisible');
        iframe.src = "~/image/product/laptop.jpg";
    }

    $scope.getRequestedDocs = function () {

        ViewRequestFactory.GetRequestedDocs($scope.OrderID)
            .then(function (response) {
             //   console.log(response.data[0])
                $scope.UploadedFiles = response.data[0];
            });
    }
    $scope.getRequestedDocs();


    //Bind Grand Code Data

   


    $scope.GoToEditGrantCode = function (val) {
        debugger
        $window.localStorage.setItem("IsEdit", "True");
        $window.location.href = '/AddNewGrantCode/AddMultipleGrantCodeEdit?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    $scope.CalcTotalAmt = function (AmtData) {
        var TotalAmt = 0;
        for (var i = 0; i < AmtData.length; i++) {
            TotalAmt = 0
            for (var j = 0; j < AmtData[i].Data.length; j++) {
                if (AmtData[i].Data[j].SelectedItem != undefined && AmtData[i].Data[j].SelectedItem != null && AmtData[i].Data[j].SelectedItem != "") {
                    TotalAmt += AmtData[i].Data[j].SelectedItem.split('\,')[1] * AmtData[i].Data[j].SelQty;
                }
                else {
                    TotalAmt += 0;
                }
            }
            $scope.Seriallst[i].TotalAmt = TotalAmt;
        }
    }




    //email popup
    $scope.SelectSub = function (arg) {
        if (arg) {
            $scope.form.Subject = "ReferenceNo:- " + $scope.ReferenceNo;
        }
        else
        {
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
            strOrderID : $scope.OrderID ,
            Type:1
        };
        $scope.form.Subject = "";
        $scope.form.Message = "";
        $scope.form.MessageType = null;
        ViewRequestFactory.SendEmailData(Data)
            .then(function (response) {
                //alert(response.data);
                if (response.data.length != 0) {
                    alert('Request has been sent successfully.');
                }
            });
    };

    $scope.PrintPdf = function ()
    {
        printDiv($scope.BudgetGrantShowHide);
    }

    function printDiv(val) {
        debugger;
        if (val)

        {
            var divToPrint = document.getElementById('DivIdToGrantPrint');
            var newWin = window.open('', 'Print-Window');

            newWin.document.open();

            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

            newWin.document.close();

            setTimeout(function () { newWin.close(); }, 10);

        }
        else
        {
            var divToPrint = document.getElementById('DivIdBudgetToPrint');
            var newWin = window.open('', 'Print-Window');

            newWin.document.open();

            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

            newWin.document.close();

            setTimeout(function () { newWin.close(); }, 10);
        }
   
       
      

    }
    $scope.PrintExcel = function () {
        debugger;
       // download_excel($scope.BudgetGrantShowHide, $scope.ReferenceNo);
        var val = $scope.BudgetGrantShowHide;
        var Reference_NO = $scope.ReferenceNo;
        if (val) {
            //$("#btnExport").click(function (e) {
            $("#btnExport").attr({
                //'download': "download.xls",
                'download': Reference_NO + ".xls",
                'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvGrantData').html())
            });
          //  });
        }
        else {
            //$("#btnExport").click(function (e) {
            $("#btnExport").attr({
                'download': Reference_NO + ".xls",
                    // 'download': Reference_NO + ".xls",

                    'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvBudgetData').html())
                })
            //});
        }

    }
 
    //function download_excel(val, Reference_NO) {
    //    debugger;
    //    if (val) {
    //        $("#btnExport").click(function (e) {
    //            $(this).attr({
    //                //'download': "download.xls",
    //                'download': Reference_NO + ".xls",
    //                'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvGrantData').html())
    //            })
    //        });
    //    }
    //    else {
    //        $("#btnExport").click(function (e) {
    //            $(this).attr({
    //                'download': "download.xls",
    //               // 'download': Reference_NO + ".xls",
              
    //                 'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvBudgetData').html())
    //            })
    //        });
    //    }
       

    //}
    

    //$("#btnExport").click(function (e) {
    //    debugger;
    //    var val = $scope.BudgetGrantShowHide;
    //    var Reference_NO = $scope.ReferenceNo;
    //    if (val) {
    //        $(this).attr({
    //            //'download': "download.xls",
    //            'download': Reference_NO + ".xls",
    //            'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvGrantData').html())
    //        })
    //    }
    //    else {
    //        $(this).attr({
    //           // 'download': "download.xls",
    //             'download': Reference_NO + ".xls",

    //            'href': 'data:application/csv;charset=utf-8,' + encodeURIComponent($('#dvBudgetData').html())
    //        })
    //    }
        
    //});

}]);