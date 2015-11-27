(function() {

    angular
        .module("onlineCardGames")
        .controller("homeController", HomeController);

    function HomeController($scope, pushService) {
        var vm = this;
        vm.onlinePlayers = 0;

        pushService.on("numberOfPlayersOnline", function (onlinePlayers) {
            vm.onlinePlayers = onlinePlayers;
        });

        pushService.initialise().then(function () {
            pushService.joinLobby();
        });
    }

})();