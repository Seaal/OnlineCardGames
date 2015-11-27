(function() {

    angular
        .module("onlineCardGames")
        .controller("homeController", HomeController);

    function HomeController($scope, pushService) {
        var vm = this;
        vm.onlinePlayers = 0;
        vm.games = [];

        pushService.on("updateOnlinePlayers", function (onlinePlayers) {
            vm.onlinePlayers = onlinePlayers;
        });

        pushService.on("updateGameList", function(games) {
            vm.games = games;
        });

        pushService.initialise().then(function () {
            pushService.joinLobby();
        });
    }

})();