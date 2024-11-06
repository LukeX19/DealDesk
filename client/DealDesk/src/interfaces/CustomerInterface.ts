export interface CustomerRequest {
  name: string;
  discountStrategies: string[];
}

export interface Customer {
  id: number;
  name: string;
  discountStrategies: string[];
}