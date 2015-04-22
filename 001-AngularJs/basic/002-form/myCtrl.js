app.controller("myCtrl", function($scope){
    $scope.master = { firstName:"John", lastName:"Doe" };
    $scope.reset = function(){
        $scope.user = angular.copy($scope.master);
    };
});