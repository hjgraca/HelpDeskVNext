helpdesk.kanbanBoardApp.controller('boardCtrl', function ($scope, boardService, $uibModal, $rootScope, userId) {
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
           }, $scope.onError);
    };

    $scope.dragControlListeners = {
        accept: function (sourceItemHandleScope, destSortableScope) {
            //debugger;
            return true;
        },
        itemMoved: function (event) {
            var targetColId = event.dest.sortableScope.$parent.columns[event.dest.sortableScope.$parent.$index].Id;
            boardService.updateTicket(event.source.itemScope.modelValue, targetColId).then(function () {
                $scope.onSuccess("Ticket movido com sucesso");
            }, $scope.onError);

            $scope.isLoading = true;
        },
        orderChanged: function (event) {
            var values = [];
            $.each(event.source.itemScope.sortableScope.modelValue, function (e, x) {
                values.push(x.TicketId);
            });

            boardService.updatePosicoes(values).then(function () {
                $scope.onSuccess("Ticket movido com sucesso");
            }, $scope.onError);

            $scope.isLoading = true;
        },
        containment: '.body-content'
    };

    $scope.createTicket = function () {
        $scope.formData.Posicao = $scope.columns[0].Tickets.length + 1;
        boardService.createTicket($scope.formData).then(function () {
            $scope.onSuccess("Ticket criado com sucesso!");

            $('#createTicketModal').modal('hide');

        }, $scope.onError);

        $scope.isLoading = true;
    };

    $scope.open = function (ticket) {

        var modalInstance = $uibModal.open({
            //animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            scope: $scope,
            //size: size,
            resolve: {
                ticket: function () {
                    return ticket;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.delete = function (ticket) {
        if (confirm("Vai apagar o ticket, prosseguir?")) {
            boardService.deleteTicket(ticket.TicketId).then(function () {
                $scope.onSuccess("Ticket apagado com sucesso!");
            }, $scope.onError);

            $scope.isLoading = true;
        }
    };

    $scope.meuTicket = {};
    $scope.meusTicketsFilter = function (tick) {
        if (!$scope.meuTicketApenas) return tick;
        if (tick.TecnicoId === userId || tick.CreatedByUtilizadorId === userId)
            return tick;
        return;
    }

    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();
        //toastr.success(e, "Sucesso");
    });

    $rootScope.$on("success", function (e, msg) {
        $scope.isLoading = false;
        $scope.onSuccess(msg);
    });

    $scope.onSuccess = function (message) {
        $scope.refreshBoard();
        toastr.success(message, "Sucesso");
    };

    $scope.onError = function (errorMessage) {
        $scope.isLoading = false;
        toastr.error(errorMessage, "Erro!");
    };

    init();
});

helpdesk.kanbanBoardApp.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, ticket,
    departamentos, estados, tecnicos, prioridades, boardService, $rootScope) {

    $scope.ticket = ticket;
    $scope.departamentos = _.map(JSON.parse(departamentos), function (v) { return { DepartamentoId: v.Value, Nome: v.Text }; });
    $scope.estados = _.map(JSON.parse(estados), function (v) { return { EstadoId: v.Value, Designacao: v.Text }; });
    $scope.tecnicos = _.map(JSON.parse(tecnicos), function (v) { return { Id: v.Value, Nome: v.Text }; });
    $scope.prioridades = _.map(JSON.parse(prioridades), function (v) { return { PrioridadeId: v.Value, Designacao: v.Text }; });

    $scope.ok = function () {

        boardService.updateTicket($scope.ticket).then(function () {
            $rootScope.$emit('success', "Ticket: " + $scope.ticket.TicketId + " actualizado");
        }, $scope.$parent.onError);

        $scope.$parent.isLoading = true;

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});