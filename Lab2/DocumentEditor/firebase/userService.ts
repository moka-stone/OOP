import {
    doc,
    setDoc,
    getDoc,
    collection,
    query,
    getDocs
} from 'firebase/firestore';
import {
    createUserWithEmailAndPassword,
    signInWithEmailAndPassword,
    signOut
} from 'firebase/auth';
import { db, auth } from './config';

export interface User {
    id: string;
    email: string;
    isAdmin: boolean;
    createdAt: Date;
}

export const userService = {
    async createUser(email: string, password: string, adminCode?: string): Promise<User> {
        const userCredential = await createUserWithEmailAndPassword(auth, email, password);
        const user: User = {
            id: userCredential.user.uid,
            email: email,
            isAdmin: adminCode === '0000',
            createdAt: new Date()
        };

        await setDoc(doc(db, 'users', user.id), user);
        return user;
    },

    async loginUser(email: string, password: string): Promise<User> {
        const userCredential = await signInWithEmailAndPassword(auth, email, password);
        const userDoc = await getDoc(doc(db, 'users', userCredential.user.uid));
        return userDoc.data() as User;
    },

    async logoutUser(): Promise<void> {
        await signOut(auth);
    },

    async getCurrentUser(): Promise<User | null> {
        if (!auth.currentUser) return null;
        const userDoc = await getDoc(doc(db, 'users', auth.currentUser.uid));
        return userDoc.data() as User;
    },

    async isUserAdmin(userId: string): Promise<boolean> {
        const userDoc = await getDoc(doc(db, 'users', userId));
        const userData = userDoc.data() as User;
        return userData?.isAdmin || false;
    }
}; 