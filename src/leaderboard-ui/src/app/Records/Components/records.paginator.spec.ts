/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { RecordsPaginatorComponent } from './records.paginator';

describe('RecordsPaginatorComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        RecordsPaginatorComponent
      ],
    });
  });

  it('should create the component', async(() => {
    let fixture = TestBed.createComponent(RecordsPaginatorComponent);
    let component = fixture.debugElement.componentInstance;
    expect(component).toBeTruthy();
  }));

  // it(`should have as title 'app works!'`, async(() => {
  //   let fixture = TestBed.createComponent(RecordsPaginatorComponent);
  //   let app = fixture.debugElement.componentInstance;
  //   expect(app.title).toEqual('app works!');
  // }));

  // it('should render title in a h1 tag', async(() => {
  //   let fixture = TestBed.createComponent(RecordsPaginatorComponent);
  //   fixture.detectChanges();
  //   let compiled = fixture.debugElement.nativeElement;
  //   expect(compiled.querySelector('h1').textContent).toContain('app works!');
  // }));
});
