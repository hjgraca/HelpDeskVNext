helpdesk.kanbanBoardApp.controller('boardCtrl', function ($scope, boardService) {
    // Model
    $scope.columns = [];
    $scope.isLoading = false;
    $scope.formData = {};
    $scope.ticketData = {};

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

    $scope.dragControlListeners = {
        accept: function (sourceItemHandleScope, destSortableScope) {
            //debugger;
            return true;
        },
        itemMoved: function(event) {
            var targetColId = event.dest.sortableScope.$parent.columns[event.dest.sortableScope.$parent.$index].Id;
            boardService.updateTicket(event.source.itemScope.modelValue, targetColId).then(function () {
                $scope.isLoading = false;
                onSuccess("Ticket movido com sucesso");
            }, onError);

            $scope.isLoading = true;
        },
        orderChanged: function (event) {
            //event.dest.sortableScope.$parent.columns
            //event.source.itemScope.modelValue
            var values = [];
            $.each(event.source.itemScope.sortableScope.modelValue, function (e, x) {
                values.push(x.TicketId);
            });
            boardService.updatePosicoes(values).then(function () {
                $scope.isLoading = false;
                //onSuccess("Ticket movido com sucesso");
            }, onError);

            $scope.isLoading = true;
        }
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
        $scope.formData.Posicao = $scope.columns[0].Tickets.length + 1;
        boardService.createTicket($scope.formData).then(function () {
            $scope.isLoading = false;
            onSuccess("Ticket criado com sucesso!");
            //boardService.sendRequest();
        }, onError);

        $scope.isLoading = true;
    };

    $scope.editTicket = function (ticket) {
        $scope.ticketData = ticket;
    };

    // Listen to the 'refreshBoard' event and refresh the board as a result
    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();
        //toastr.success(e, "Sucesso");
    });

    var onSuccess = function (message) {
        $scope.refreshBoard();
        toastr.success(message, "Sucesso");
    };

    var onError = function (errorMessage) {
        $scope.isLoading = false;
        toastr.error(errorMessage, "Erro!");
    };

    init();
});