helpdesk.kanbanBoardApp.service('boardService', function ($http, $q, $rootScope) {
    var proxy = null;

    var getColumns = function () {
        return $http.get("/api/BoardWebApi").then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data);
        });
    };

    var canMoveTicket = function (sourceColIdVal, targetColIdVal) {
        return sourceColIdVal != targetColIdVal;
    };

    var updateTicket = function (ticket, targetColIdVal) {
        ticket.Estado = null;
        ticket.EstadoId = targetColIdVal;
        return $http.put("/api/BoardWebApi/" + ticket.TicketId, ticket)
            .then(function (response) {
                return response.status == 204;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var createTicket = function (data) {
        return $http.post("/api/BoardWebApi/", data)
            .then(function (response) {
                return response.status == 201;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    }

    var initialize = function () {

        //connection = jQuery.hubConnection();
        //this.proxy = connection.createHubProxy('KanbanBoard');

        //// Listen to the 'BoardUpdated' event that will be pushed from SignalR server
        //this.proxy.on('BoardUpdated', function () {
        //    $rootScope.$emit("refreshBoard");
        //});

        //// Connecting to SignalR server        
        //return connection.start()
        //.then(function (connectionObj) {
        //    return connectionObj;
        //}, function (error) {
        //    return error.message;
        //});
    };

    // Call 'NotifyBoardUpdated' on SignalR server
    var sendRequest = function () {
        //this.proxy.invoke('NotifyBoardUpdated');
        $rootScope.$emit("refreshBoard");
    };

    return {
        initialize: initialize,
        sendRequest: sendRequest,
        getColumns: getColumns,
        canMoveTicket: canMoveTicket,
        updateTicket: updateTicket,
        createTicket: createTicket
    };
});