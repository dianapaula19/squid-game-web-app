import React from 'react';
import { ComponentStory, ComponentMeta } from '@storybook/react';

import { UserRegistrationForm } from './UserRegistrationForm';

export default {
  title: 'Forms/UserRegistrationForm',
  component: UserRegistrationForm,
} as ComponentMeta<typeof UserRegistrationForm>;

const Template: ComponentStory<typeof UserRegistrationForm> = (args) => <UserRegistrationForm {...args}/>;

export const Primary = Template.bind({});