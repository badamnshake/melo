export interface Member {
  username: string;
  gender: string;
  dateOfBirth: string;
  knownAs: string;
  created: Date;
  lastActive: Date;
  introduction: string;
  lookingFor: string;
  interests: string;
  age: string;
  city: string;
  country: string;
  photos: Photo[];
}

export interface Photo {
  id: number;
  url: string;
  isMain: boolean;
}
