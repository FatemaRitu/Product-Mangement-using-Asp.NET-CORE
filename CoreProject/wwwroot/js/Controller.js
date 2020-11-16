app.controller('myController', function ($scope, myService) {

    GetCategoryFC();
    $scope.newCategory = {};
    $scope.clickedCategory = {};
    $scope.message = ""; 

    function GetCategoryFC() {
        myService.getCategory().then(function (result) {
            $scope.Category = result.data;
            console.log(result.data);
        });
    };

    $scope.SaveCategory = function () {
        myService.addCategory($scope.newCategory)
            .then(function (result) {
                $scope.newCategory = {};
                $scope.message = "Data saved successfully";
                GetCategoryFC()
            }, function (result) {
            alert("not saved");
       });
    };
    $scope.SelectedCategory = function (Category) {
        $scope.clickedCategory = Category;        
    };
    $scope.UpdateCategory = function () {
        myService.updateCategory($scope.clickedCategory).then(function (result) {
            GetCategoryFC();
            $scope.message = "Updated Successfully";
        }, function (result) {
            $scope.message = "Updated failed";
        })       
    };

    $scope.DeleteCategory = function () {
        
        myService.deleteCategory($scope.clickedCategory.CategoryId).then(function (result) {
            GetCategoryFC();
            $scope.message = "Deleted Successfully";
        }, function (result) {
            $scope.message = "Error Occured";
        })      
    };
    $scope.ClearMessage = function () {
        $scope.message = "";
    };  
})