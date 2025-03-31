
export interface Product {
    id?: string;
    name: string;
    itemCode: string;
    imageUrl: string;
    description: string;
    quantity: string;
    ingredients: string;
    howToUse: string;
    selfLife: string;
    mrp: number;
    dp: number;
    bValue: number;
    categoryId?: number;
    category?: string;
    subCategory?: string;
    featureList: string[];
    imageList: string[];
    status: boolean;
}