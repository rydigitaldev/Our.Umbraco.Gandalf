app.requires.push('ngTable');

/*
* IpLocker CONTROLLER
* -----------------------------------------------------
* Main controller used to render out the content section
*/
angular.module("umbraco").controller("IpLockerController", function ($scope, $filter, IpLockerApi, ngTableParams) {

    //Property to display error messages
    $scope.errorMessage = '';
    //App state
    $scope.initialLoad = false;
    $scope.cacheCleared = false;

    $scope.refreshTable = function () {
        //If we aren't set up yet, return
        if (!$scope.tableParams) return;

        $scope.tableParams.total($scope.items.length);
        $scope.tableParams.reload();
    }

    /*
    * Handles clearing the cache by
    * calling to get all items again
    */
    $scope.clearCache = function () {
        $scope.cacheCleared = true;
        return IpLockerApi.clearCache().then($scope.fetchItems.bind(this));
    }

    /*
    * Handles fetching all items from the server.
    */
    $scope.fetchItems = function () {
        return IpLockerApi.getAll().then($scope.onRecieveAllItemsResponse.bind(this));
    };

    /*
    * Response handler for requesting all items
    */
    $scope.onRecieveAllItemsResponse = function (response) {
        //Somethign went wrong. Error out
        if (!response || !response.data) {
            $scope.errorMessage = "Error fetching items from server";
            return;
        }

        //We received data. Continue
        $scope.items = response.data;
        $scope.refreshTable();
    }

    /*
    * Handles adding a new item to the collection.
    * Sends request off to API.
    */
    $scope.addItem = function (item) {
        IpLockerApi.add(item.IpAddress, item.Notes)
            .then($scope.onAddItemResponse.bind(this));
    };

    /*
    * Handles the Add Item response from the API. Checks
    * for errors and updates table.
    */
    $scope.onAddItemResponse = function (response) {
        //Check for error
        if (!response || !response.data) {
            $scope.errorMessage = "Error sending request to add a new item.";
            return;
        }

        //Handle success from API
        if (response.data.Success) {
            $scope.errorMessage = '';
            $scope.items.push(response.data.Item);
            $scope.refreshTable();
        }
        else {
            $scope.errorMessage = response.data.Message;
        }
    }

    /*
    * Handles sending to the API to as a reference for
    * updating the items collection server side.
    */
    $scope.updateItem = function (item) {
        IpLockerApi.update(item).then($scope.onUpdateItemResponse.bind(this, item));
    }

    /*
    * Handler for receiving a response from the Update Item API call
    * Will update the table with the returned, updated item
    */
    $scope.onUpdateItemResponse = function (item, response) {
        //Check for error
        if (!response || !response.data) {
            $scope.errorMessage = "Error sending request to update a item.";
            return;
        }

        if (response.data.Success) {
            $scope.errorMessage = '';
            item.LastUpdated = response.data.Item.LastUpdated;
            item.$edit = false;
        }
        else {
            $scope.errorMessage = response.data.Message;
        }
    }

    /*
    * Handles the delete request to delete item.
    * Calls the Delete API method passing in the item ID
    */
    $scope.deleteItem = function (item) {
        if (confirm("Are you sure you want to delete this item?")) {
            IpLockerApi.remove(item.Id).then($scope.onDeleteItemResponse.bind(this, item));
        }
    }

    /*
    * Handles the DeleteItem response from the API. If successful,
    * remove the item from the table.
    */
    $scope.onDeleteItemResponse = function (item, response) {
        //Check for error
        if (!response || !response.data) {
            $scope.errorMessage = "Error sending request to delete item.";
            return;
        }

        //Remove the item from the table. Splice item array
        if (response.data.Success) {
            $scope.errorMessage = '';
            var index = $scope.items.indexOf(item);
            if (index > -1) {
                $scope.items.splice(index, 1);
                $scope.tableParams.total($scope.items.length);
                $scope.tableParams.reload();
            }

        }
        else {
            $scope.errorMessage = response.data.errorMessage;
        }
    }

    /*
    * Clears the global error message
    */
    $scope.clearErrorMessage = function () {
        $scope.errorMessage = '';
    }

    /*
    * Defines a new ngTable. 
    */
    $scope.tableParams = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        sorting: {
            LastUpdated: 'desc'     // initial sorting
        },
        filter: {
            Message: ''       // initial filter
        },
        data: $scope.initialData
    }, {
        total: 0,
        getData: function ($defer, params) {

            //Do we have items yet?
            var data = $scope.items || [];

            //Do we have a search term set in the search box?
            //If so, filter the items for that text
            var searchTerm = params.filter().Search;
            var searchedData = searchTerm ?
                data.filter(function (item) {
                    return item.Notes.toLowerCase().indexOf(searchTerm.toLowerCase()) > -1 ||
                        item.IpAddress.toLowerCase().indexOf(searchTerm.toLowerCase()) > -1
                }) : data;

            //Are we ordering the results?
            var orderedData = params.sorting() ?
                    $filter('orderBy')(searchedData, params.orderBy()) :
                    searchedData;

            //Set totals and page counts
            params.total(orderedData.length);
            var pagedResults = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

            //Cheat and add a blank item so the user can add a new item right from the table
            pagedResults.push({ Id: "-1", IpAddress: "", Notes: "", LastUpdated: "", $edit: true });
            $defer.resolve(pagedResults);
        }
    })

    /*
    * Initial load function to set loaded state
    */
    $scope.initLoad = function () {
        if (!$scope.initialLoad) {
            //Get the available log dates to view log entries for.
            $scope.fetchItems()
                .then(function () { $scope.initialLoad = true; });
        }
    }

    $(function () {
        $scope.$tab = $('a:contains("Manage Items")');

        //If we have a tab, set the click handler so we only
        //load the content on tab click. 
        if ($scope.$tab && $scope.$tab.length > 0) {
            var $parent = $scope.$tab.parent();

            // bind click event
            $scope.$tab.on('click', $scope.initLoad.bind(this));

            // if it is selected already or there is only one tab, init load
            if ($parent.hasClass('active') || $parent.children().length == 1)
                $scope.initLoad();
        }
        else {
            $scope.initLoad();
        }
    });

});

/*
* IpLockerAPI
* -----------------------------------------------------
* Resource to handle making requests to the backoffice API to handle CRUD operations
*/
angular.module("umbraco.resources").factory("IpLockerApi", function ($http) {
    return {
        //Get all from the server
        getAll: function () {
            return $http.get("backoffice/IpLocker/AllowedIpApi/GetAll");
        },
        //Send data to add a new
        add: function (ipAddress, notes) {
            return $http.post("backoffice/IpLocker/AllowedIpApi/Add", JSON.stringify({ ipAddress: ipAddress, notes: notes }));
        },
        //Send request to update an existing
        update: function (item) {
            console.log(item);
            return $http.post("backoffice/IpLocker/AllowedIpApi/Update", JSON.stringify({ Item: item }));
        },
        //Remove / Delete an existing 
        remove: function (id) {
            return $http.delete("backoffice/IpLocker/AllowedIpApi/Delete/" + id);
        },

        //Clear cache
        clearCache: function () {
            return $http.post("backoffice/IpLocker/AllowedIpApi/ClearCache");
        }
    };
});
