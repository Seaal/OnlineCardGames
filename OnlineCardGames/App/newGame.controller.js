(function() {

    angular
        .module("onlineCardGames")
        .controller("newGameController", NewGameController);

    function NewGameController($state, pushService) {
        var vm = this;

        vm.name = "";
        vm.chipCount = 1500;
        vm.maxPlayers = 6;
        vm.createGame = createGame;

        function createGame() {
            var game = {
                name: vm.name,
                initialChipCount: vm.chipCount,
                maxPlayers: vm.maxPlayers
            };

            pushService.createGame(game).then(function(gameId) {
                $state.go("game", { id: gameId });
            });
        }
    }

})();