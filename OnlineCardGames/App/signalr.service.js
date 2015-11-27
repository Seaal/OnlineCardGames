(function() {

    angular
        .module("onlineCardGames")
        .factory("pushService", pushService);

    function pushService($rootScope, $q) {

        var service = {
            initialise: initialise,
            joinLobby: joinLobby,
            on: on
        };

        var proxy;

        function initialise() {
            var connection = $.hubConnection();
            
            proxy = connection.createHubProxy("pokerHub");

            return $q.when(connection.start());
        }

        function joinLobby() {
            proxy.invoke("joinLobby");
        }

        function on(eventName, callback) {
            proxy.on(eventName, function (result) {
                $rootScope.$apply(function () {
                    if (callback) {
                        callback(result);
                    }
                });
            });
        }

        return service;
    }

})();