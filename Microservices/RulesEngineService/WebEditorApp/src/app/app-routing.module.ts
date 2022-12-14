import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ViewWorkflowComponent } from './view-workflow/view-workflow.component';
import { WorkflowTableComponent } from './workflow-table/workflow-table.component';

const appRoutes: Routes = [
  // { path: 'search', component: SearchFilterComponent },
  // {
  //   path: 'payment-success',
  //   component: PaymentSuccessPageComponent,
  //   canActivate: [AuthGuard, CustomerAuthGuard],
  // },

  // { path: 'seller-login', component: SellerLoginComponent },
  // { path: 'login', component: LoginPageComponent },
  // { path: 'register', component: RegisterPageComponent },
  // {
  //   path: 'my-orders',
  //   component: MyOrdersComponent,
  //   canActivate: [AuthGuard, CustomerAuthGuard],
  // },
  // {
  //   path: 'my-coupons',
  //   component: MyCouponsComponent,
  //   canActivate: [AuthGuard, CustomerAuthGuard],
  // },
  // {
  //   path: 'user-profile',
  //   component: UserProfileComponent,
  //   canActivate: [AuthGuard, CustomerAuthGuard],
  // },
  // { path: 'product/:id', component: ProductDetailsComponent },
  // {
  //   path: 'basket',
  //   loadChildren: () =>
  //     import('./basket/basket.module').then((mod) => mod.BasketModule),
  //   data: { breadcrumb: 'Basket' },
  //   canActivate: [AuthGuard, CustomerAuthGuard],
  // },
  { path: '', component: WorkflowTableComponent, pathMatch: 'full' },
  { path: ':workflowId', component: ViewWorkflowComponent },
  // {
  //   path: 'seller-profile',
  //   component: SellerProfileComponent,
  //   canActivate: [AuthGuard, SellerAuthGuard],
  // },
  // {
  //   path: 'seller-panel',
  //   component: SellerPanelComponent,
  //   canActivate: [AuthGuard, SellerAuthGuard],
  //   children: [
  //     { path: 'dashboard', component: SellerDashboardTabComponent },
  //     { path: 'manage-products', component: SellerManageproductsTabComponent },
  //     { path: 'manage-orders', component: SellerManageordersTabComponent },
  //   ],
  // },
  // {
  //   path: 'admin-panel',
  //   component: AdminPanelComponent,
  //   canActivate: [AuthGuard, AdminAuthGuard],
  //   children: [
  //     { path: 'dashboard', component: AdminDashboardTabComponent },
  //     { path: 'manage-clients', component: AdminManageClientsTabComponent },
  //     { path: 'manage-sellers', component: AdminManageSellersTabComponent },
  //     { path: 'manage-coupons', component: AdminManageCouponsTabComponent },
  //     {
  //       path: 'manage-categories',
  //       component: AdminManageCategoriesTabComponent,
  //     },
  //   ],
  // },
  // { path: ':name', component: CategoriesPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
