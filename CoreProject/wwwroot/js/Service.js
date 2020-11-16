app.service('myService', function ($http) {
        this.getCategory = function () {
        var response = $http.get('Category/GetAllCategory');
        return response;
    };
        this.addCategory = function (Category) {
        var response = $http({
            method: 'post',
            url: 'Category/AddCategory',
            data: JSON.stringify(Category)
        });
        return response;
    };
        this.updateCategory = function (Category) {
        var response = $http({
            method: 'post',
            url: 'Category/UpdateCategory/',
            data: JSON.stringify(Category)
        });
        return response;
    };
    this.deleteCategory = function (id) {
        var response = $http({
            method: 'post',
            url: 'Category/DeleteCategory',
            params: { Id: JSON.stringify(id) }
        });
        return response;
    };
  });