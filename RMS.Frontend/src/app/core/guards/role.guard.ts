import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state) => {
  const router = inject(Router);
  
  // 1. app.routing.ts içinde bu rota için hangi rollere izin verildiğini okuyoruz
  const expectedRoles = route.data['roles'] as Array<string>;
  
  // 2. Mevcut kullanıcının rolü. 
  // NOT: Gerçek senaryoda burası "const currentRole = inject(AuthService).getUserRole();" şeklinde AuthService'den veya Token'dan gelir.
  // Şimdilik sistemin çalışması ve test edebilmemiz için statik olarak 'Employee' atıyoruz.
  const currentRole = 'Employee'; 

  // 3. Eğer rota için özel bir rol kısıtlaması yoksa veya kullanıcının rolü beklenen roller içindeyse geçişe izin ver
  if (!expectedRoles || expectedRoles.includes(currentRole)) {
    return true;
  }

  // 4. Kullanıcının yetkisi yoksa (Örn: Employee, Manager sayfasına girmeye çalışırsa) onu yetkisiz sayfasına veya ana sayfaya postala
  // createUrlTree kullanımı, Angular router'da güvenli yönlendirme (Best Practice) için tercih edilir.
  return router.createUrlTree(['/unauthorized']); 
};