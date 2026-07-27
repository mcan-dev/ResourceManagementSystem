import { TestBed } from '@angular/core/testing';

import { PageHeader } from './page-header';

describe('PageHeader', () => {
  let service: PageHeader;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageHeader);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
