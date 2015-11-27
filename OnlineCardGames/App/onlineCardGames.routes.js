(function() {

    angular
        .module("onlineCardGames")
        .config(routes);

    function routes($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "App/home.view.html",
                controller: "homeController as home"
            })
            .state("newGame", {
                url: "/newGame",
                templateUrl: "App/newGame.view.html",
                controller: "newGameController as newGame"
            })
            .state("game", {
                url: "/game/{id}",
                templateUrl: "App/game.view.html",
                controller: "gameController as game"
            });
    }

})();