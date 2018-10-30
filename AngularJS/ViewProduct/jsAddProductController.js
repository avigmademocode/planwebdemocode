HomeApp.controller('AddProductController', ['$scope', '$window', 'AddProductFactory', function ($scope, $window, AddProductFactory) {

    //$scope.patient = {};
    //$scope.patient.BasicDetails = {};

    $scope.ProductID = $scope.ProductID ? $scope.ProductID.split('?')[1] : window.location.search.slice(8);
    $scope.isDisabled;
    $scope.isTierDisabled = true;
    //alert($scope.OrderID)
    //$scope.ManufactureList = {};
    $scope.TireData = [];
   
    $scope.GetProductsData = function () {
       // debugger;
        ResetData();


        if ($scope.ProductID != '') {
            //debugger
            //$scope.BindDataByProductID;
            $scope.isTierDisabled = false;
            AddProductFactory.GetProductsDataList()
                .then(function (response) {
                    if (response.data.length != 0) {
                        //debugger;
                        $scope.custList = response.data[0][0];
                        $scope.catList = response.data[1][1];
                        $scope.ManufactureList = response.data[2][0];
                        $scope.ProdtypeList = response.data[2][1];
                        if (response.data[3] == "True")
                        {
                            $scope.isDisabled = false;
                        }
                        else
                        {
                            $scope.isDisabled = true;
                        }

                        var data =
                            {
                                ProductID: $scope.ProductID
                            }
                        AddProductFactory.getDataByProductID(data)
                            .then(function (response) {
                                if (respond.data != '') {
                                    //debugger;
                                    $scope.Model = response.data[0][0].Model;
                                    $scope.PartNo = response.data[0][0].PartNo;
                                    $scope.GetTireData = response.data[3];

                                    var index = $scope.ManufactureList.findIndex(obj => obj.ManufacturerId == response.data[0][0].ManufacturerId);
                                    if (index >= 0) {
                                        $scope.Manufacturer = $scope.ManufactureList[index].ManufacturerId;
                                    }
                                    var indextype = $scope.ProdtypeList.findIndex(obj => obj.ProductTypeId == response.data[0][0].ProductTypeId);
                                    if (indextype >= 0) {
                                        $scope.ProductType = $scope.ProdtypeList[indextype].ProductTypeId;
                                    }


                                    for (var i = 0; i < response.data[1].length; i++) {
                                        //debugger
                                        for (var j = 0; j < $scope.custList.length; j++) {
                                            if (response.data[1][i].CustId == $scope.custList[j].CustId) {
                                                $scope.custList[j].Price = response.data[1][i].Price;
                                                $scope.custList[j].PCRId = response.data[1][i].PCRId;
                                                $scope.custList[j].IsActive = response.data[1][i].IsActive;
                                                var dateOut = new Date(response.data[1][i].ExpDate);
                                                $scope.custList[j].ExpDate = dateOut;
                                                var index = $scope.catList.findIndex(obj => obj.ProdCatId == response.data[1][i].ProdCatId);
                                                if (index >= 0) {
                                                    $scope.custList[j].ProdCatId = $scope.catList[index].ProdCatId;
                                                }

                                            }
                                        }
                                    }
                                    if (response.data[2].length > 0) {
                                        $scope.Spec = response.data[2][0].Spec;
                                        $scope.ImageID = response.data[2][0].ImageID;
                                    }

                                }
                                //console.log($scope.ManufactureList)
                            });


                        //console.log(response.data)
                    }
                });
        
        }
        else {
            AddProductFactory.GetProductsDataList()
                .then(function (response) {
                    
                    if (response.data.length != 0) {
                       //debugger;
                        $scope.custList = response.data[0][0];
                        $scope.catList = response.data[1][1];
                        $scope.ManufactureList = response.data[2][0];
                        $scope.ProdtypeList = response.data[2][1];
                    }
                });
        }

    }
    $scope.GetProductsData();

    $scope.Add = function () {
        //Add the new item to the Array.
        var fileObj = {};
        fileObj.ImageUpload = $scope.ImageUpload;
      
        $scope.files.push(fileObj);

        //Clear the TextBoxes.
        $scope.ImageUpload = "";
    };
    $scope.Add();

    $scope.files = [];
    $scope.SelectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
        for (var i = 0; i < file.length; i++) {
            $scope.files.push(file[i])
        }
    }
    //$scope.ProdData = '';
    $scope.SaveProductDtls = function (proddata) {
        //debugger;

        /*
        var Dataval =
            {
                Model : $scope.Model,
                PartNo : $scope.PartNo,
                Spec: $scope.Spec,
                Manufacturer : $scope.Manufacturer,
                ProductType : $scope.ProductType,
                custlist: JSON.stringify(proddata)
            }
            */
        var manf = 0;
        if ($scope.Manufacturer != undefined)
        {
            manf = $scope.Manufacturer;
        }
        var prodtype = 0;
        if ($scope.ProductType != undefined) {
            prodtype = $scope.ProductType;
        }
        //if ($scope.files.length != 0) {
        var data = new FormData();
        if ($scope.files.length != 0) {
            $scope.FileDescription = "";
           
            for (var i = 0; i < $scope.files.length; i++) {
                data.append("files[" + i + "]", $scope.files[i])
                data.append("Model", $scope.Model);
                data.append("PartNo", $scope.PartNo);
                data.append("Spec", $scope.Spec);
                data.append("Manufacturer", manf);
                data.append("ProductType", prodtype);
                data.append("ProductID", $scope.ProductID);
                data.append("custlist", JSON.stringify(proddata));
            }
        }
        else
        {
            data.append("files[" + i + "]", $scope.files[i])
            data.append("Model", $scope.Model);
            data.append("PartNo", $scope.PartNo);
            data.append("Spec", $scope.Spec);
            data.append("Manufacturer", manf);
            data.append("ProductType", prodtype);
            data.append("ProductID", $scope.ProductID);
            data.append("custlist", JSON.stringify(proddata));
        }
        AddProductFactory.UploadFile(data)
            .then(function (response) {
                debugger
                $scope.UploadedFiles = response.data[0];
                $scope.ProductID = response.data[0];
                alert('Data Saved successfully...');
                if ($scope.ProductID != "") {
                    $scope.isTierDisabled = false;
                }
                //$window.location.reload();
                $scope.Add();
               // ResetData();
                $scope.GetProductsData();
                $scope.form.CustomerId = null;
            });
        //}
        //else
        //{
        //    alert('Please Select Image to Upload');
        //}
    }

    function ResetData() {
        $scope.Model = '';
        $scope.PartNo = '';
        $scope.Spec = '';
        $scope.SelectedFileForUpload = null;
       // $scope.ManufactureList = null;
       // $scope.ProdtypeList = null;

        $scope.files = [];
        $scope.uploadfiles = [];
        //angular.element("#fileupload").value = "";
       
    }

 
    $scope.Back = function ()
    {
        $window.location.href = '/ViewProduct';
    }

    //Update Code



    ///// Tier popup data

    $scope.GetCustData = function (Custid) {
        $scope.GetProductsData();
        debugger
      
        if ($scope.GetTireData.length != 0) {
            //$scope.TireData = $scope.GetTireData;
            $scope.TireData = [];

            for (var i = 0; i < $scope.GetTireData.length; i++) {
                if ($scope.GetTireData[i].CustId == Custid) {

                    var obj = {};
                    obj.PCTRId = $scope.GetTireData[i].PCTRId;
                    obj.Qty = $scope.GetTireData[i].Qty;
                    obj.Price = $scope.GetTireData[i].Price;
                    obj.IsActive = $scope.GetTireData[i].IsActive;
                    //obj.EffectiveFrom = $scope.GetTireData[i].StrEffectiveFrom;


                    if ($scope.GetTireData[i].StrEffectiveFrom != null) {
                        var dateOut = new Date($scope.GetTireData[i].StrEffectiveFrom);
                        obj.EffectiveFrom  = dateOut;
                    } else {
                        obj.EffectiveFrom = null;
                    }


                    $scope.TireData.push(obj);

                }
            }




        } else {
          //  $scope.TireData = []; //{ Quantity: 0, Price: 0, Active: true, ExpDate: new Date()  }
            $scope.addRow();
        }
       
    };
    

    $scope.addRow = function () {
        //debugger
        var obj = { IsActive: true };

        obj.Active = true;
        obj.EffectiveFrom = new Date();

        $scope.TireData.push(obj);
    };
    $scope.removeRow = function (value) {
        //alert(value);

        $scope.TireData.splice(value, 1)
    };

    $scope.submitForm = function (formdata) {

        //debugger
        var TypeVAL = 1;
        //var ITSetUpIdVAL = 0;
        if (formdata[0].PCTRId != undefined) { // checking for new or not
            //ITSetUpIdVAL = $scope.form.EditNow.ITSetUpId;
            TypeVAL = 2;
        }
        var Data = {
            CustId: $scope.form.CustomerId,
            StrProductId: $scope.ProductID,
            Type: TypeVAL,
            TierDetails: JSON.stringify(formdata),
        }; 
        //alert(JSON.stringify(Data));

        //debugger;
        AddProductFactory.AddTierData(Data)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    alert('Request has been saved successfully.');

                }
                $scope.GetProductsData();
                $scope.form.CustomerId = null;
                //debugger
                $scope.TireData = [{ Quantity: 0, Price: 0, Active: true, ExpDate: new Date() }];
            });

    }
    $scope.clearData = function () {
        //debugger
      //  $scope.addRow();
       // $scope.TireData = [];
        //{ Quantity: 0, Price: 0, Active: true, ExpDate: new Date() }
    }
}]);


