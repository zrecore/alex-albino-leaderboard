import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { AppComponent } from './app.component';
import { MaterialModule } from '@angular/material';

import { RecordsTableComponent, RecordsNavigationComponent, RecordsPaginatorComponent } from './Records/Components';
import { RecordService } from './Records/Services/record.service'

@NgModule({
  declarations: [
    AppComponent,
    RecordsTableComponent,
    RecordsNavigationComponent,
    RecordsPaginatorComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    MaterialModule.forRoot(),
  ],
  providers: [
    RecordService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
