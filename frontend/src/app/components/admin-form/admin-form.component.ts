import { Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem } from '../../models/menu-item.model';

@Component({
  selector: 'app-admin-form',
  templateUrl: './admin-form.component.html',
  styleUrls: ['./admin-form.component.css']
})
export class AdminFormComponent implements OnInit, OnChanges {
  @Input() editItem: MenuItem | null = null;
  @Output() formSubmit = new EventEmitter<Omit<MenuItem, 'id'> | MenuItem>();
  @Output() cancelEdit = new EventEmitter<void>();

  form!: FormGroup;
  categories = ['Appetizer', 'Main Course', 'Dessert', 'Beverage', 'Salad', 'Pizza', 'Burger', 'Special'];

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['editItem'] && this.form) {
      if (this.editItem) {
        this.form.patchValue(this.editItem);
      } else {
        this.form.reset();
      }
    }
  }

  initForm(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      price: [null, [Validators.required, Validators.min(0.01)]],
      imageUrl: [''],
      category: ['', Validators.required]
    });
    if (this.editItem) {
      this.form.patchValue(this.editItem);
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      if (this.editItem) {
        this.formSubmit.emit({ ...this.editItem, ...this.form.value });
      } else {
        this.formSubmit.emit(this.form.value);
      }
      this.form.reset();
    } else {
      this.form.markAllAsTouched();
    }
  }

  onCancel(): void {
    this.form.reset();
    this.cancelEdit.emit();
  }

  hasError(field: string, error: string): boolean {
    const control = this.form.get(field);
    return !!(control?.hasError(error) && control?.touched);
  }
}
