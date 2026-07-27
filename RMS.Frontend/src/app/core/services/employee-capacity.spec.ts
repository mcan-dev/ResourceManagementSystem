import { TestBed } from '@angular/core/testing';

import { EmployeeCapacity } from './employee-capacity';

describe('EmployeeCapacity', () => {
  let service: EmployeeCapacity;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeCapacity);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
