import { Component, Input, Output, EventEmitter, OnInit, OnChanges } from '@angular/core';
import { MdIcon } from '@angular/material';
import { MdGridListModule } from '@angular/material/grid-list';

import { Record } from '../Models/record';
import { EmitterService } from '../../emitter.service';
import { RecordService } from '../Services/record.service';

@Component({
  selector: 'records-paginator',
  templateUrl: './records.paginator.html',
  styleUrls: ['./records.paginator.css']
})
export class RecordsPaginatorComponent implements OnInit, OnChanges {

    constructor(
        private recordService: RecordService
    ){

        if (!this.rowsPerPage) { this.rowsPerPage = 10; }
        if (!this.pagesTotal) { this.pagesTotal = 10; }
        if (!this.pageIndex) { this.pageIndex = 1; }
    }

    @Input() paginatorId: string;

    private rowsPerPage: number;
    private pagesTotal: number;
    private pageIndex: number;

    private pagination = {
        skip: 0,
        top: this.rowsPerPage,
        pagesTotal: this.pagesTotal
    };

    _lastRowsPerPage = this.rowsPerPage;
    _lastPagesTotal = this.pagesTotal;
    _lastPageIndex = this.pageIndex;

    onPagePrevious() {
      // Go back a page
      this.pageIndex--;
      if (this.pageIndex < 1) {
          this.pageIndex = 1;
      }

      console.log("onPagePrevious() pageIndex is %d", this.pageIndex);
      this.paginationChange();

    }

    onPageNext() {
      // Go to the next page
      this.pageIndex++;
      if (this.pageIndex > this.pagesTotal) {
          this.pageIndex = this.pagesTotal;
      }

      console.log("onPageNext() pageIndex is %d", this.pageIndex);
      this.paginationChange();
    }

    paginationChange() {
      let hasChange = false;
      // We want to assert emissions are only made when changes occur
      if (this._lastPageIndex !== this.pageIndex)
      {
          this._lastPageIndex = this.pageIndex;
          hasChange = true;
      }

      if (this._lastPagesTotal !== this.pagesTotal)
      {
          this._lastPagesTotal = this.pageIndex;
          hasChange = true;
      }

      if (this._lastRowsPerPage !== this.rowsPerPage)
      {
          this._lastRowsPerPage = this.rowsPerPage;
          hasChange = true;
      }

      if (hasChange === true) {
          this.pagination.top = this.rowsPerPage;
          this.pagination.skip = (this.pageIndex - 1) * this.rowsPerPage;

          EmitterService.get(this.paginatorId).emit(this.pagination);
      }
    }

    ngOnInit() {
        // Initialize stuff.
    }
    ngOnChanges() {
        this.paginationChange();
    }
}
