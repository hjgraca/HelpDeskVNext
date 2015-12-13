helpdesk.kanbanBoardApp.controller('boardCtrl', function ($scope, boardService) {
    // Model
    $scope.columns = [];
    $scope.isLoading = false;
    $scope.formData = {};

    function init() {
        $scope.isLoading = false;
        $scope.refreshBoard();
    };

    $scope.refreshBoard = function refreshBoard() {
        $scope.isLoading = true;
        boardService.getColumns()
           .then(function (data) {
               $scope.isLoading = false;
               $scope.columns = data;
           }, onError);
    };

    $scope.onDrop = function (data, targetColId) {
        if (boardService.canMoveTicket(data.EstadoId, targetColId)) {
            boardService.updateTicket(data, targetColId).then(function () {
                $scope.isLoading = false;
                onSuccess("Ticket movido com sucesso");
            }, onError);

            $scope.isLoading = true;
        }
    };

    $scope.createTicket = function () {
        boardService.createTicket($scope.formData).then(function () {
            $scope.isLoading = false;
            onSuccess("Ticket criado com sucesso!");
            //boardService.sendRequest();
        }, onError);

        $scope.isLoading = true;
    };

    // Listen to the 'refreshBoard' event and refresh the board as a result
    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();
        //toastr.success(e, "Sucesso");
    });

    var onSuccess = function(message) {
        $scope.refreshBoard();
        toastr.success(message, "Sucesso");
    };

    var onError = function (errorMessage) {
        $scope.isLoading = false;
        toastr.error(errorMessage, "Erro!");
    };

    init();
});