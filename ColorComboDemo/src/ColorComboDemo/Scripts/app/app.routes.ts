const appRoutes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent }
];

export const routing = RouteModule.forRoot(appRoutes);
