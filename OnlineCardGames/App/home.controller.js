(function() {

    angular
        .module("onlineCardGames")
        .controller("homeController", HomeController);

    function HomeController($scope, pushService) {
        var vm = this;
        vm.onlinePlayers = 0;

        pushService.initialise().then(function () {
            pushService.on("numberOfPlayersOnline", function(onlinePlayers) {
                vm.onlinePlayers = onlinePlayers;
            });

            pushService.joinLobby();
        });
    }

})();