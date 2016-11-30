import { Component } from '@angular/core';
import { MdIcon } from '@angular/material';
import { MdGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'records-paginator',
  templateUrl: './records.paginator.html',
  styleUrls: ['./records.paginator.css']
})
export class RecordsPaginatorComponent {
  recordsPerPage = 10;
  recordsTotal = 0;
  recordsIndex = 0;
}
