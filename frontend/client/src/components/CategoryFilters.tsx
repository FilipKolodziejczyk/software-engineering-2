import React, {FormEvent, Fragment, useState} from 'react'
import {Dialog, Disclosure, Menu, Transition} from '@headlessui/react'
import {XMarkIcon} from '@heroicons/react/24/outline'
import {ChevronDownIcon, FunnelIcon, MinusIcon, PlusIcon} from '@heroicons/react/20/solid'
import {Form, useSubmit} from 'react-router-dom';
import PriceRange from "./PriceRange";

const subCategories = [
  {name: 'All', value: '', current: true},
  {name: 'Roses', value: 'rose', current: false},
  {name: 'Lilies', value: 'lily', current: false},
  {name: 'Orchids', value: 'orchid', current: false},
  {name: 'Seasonal', value: 'seasonal', current: false},
  {name: 'Bouquets', value: 'bouquet', current: false},
]

const sortOptions = [
  {name: 'Newest', href: '#', current: true},
  {name: 'Price: Low to High', href: '#', current: false},
  {name: 'Price: High to Low', href: '#', current: false},
]

function classNames(...classes: string[]) {
  return classes.filter(Boolean).join(' ')
}

export function CategoryFilters({children}: { children: React.ReactNode }) {
  const [mobileFiltersOpen, setMobileFiltersOpen] = useState(false)
  const [priceRange, setPriceRange] = useState<number[]>([30, 60]);

  let submit = useSubmit();

  const handlePriceRangeChange = (event: Event, newValue: number | number[]) => {
    setPriceRange(newValue as number[]);
  }

  function submitForm() {
    let formData = new FormData();
    formData.append("minPrice", priceRange[0].toString());
    formData.append("maxPrice", priceRange[1].toString());
    const subCategory = subCategories.find((subCategory) => subCategory.current)?.value;
    if (subCategory) {
      formData.append("filteredCategory", subCategory);
    }
    submit(formData, {method: 'GET', action: '/'});
  }

  function handleFilterSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    submitForm();
  }

  return (<div className="bg-white">
    <div>
      {/* Mobile filter dialog */}
      <Transition.Root show={mobileFiltersOpen} as={Fragment}>
        <Dialog as="div" className="relative z-40 lg:hidden" onClose={setMobileFiltersOpen}>
          <Transition.Child
            as={Fragment}
            enter="transition-opacity ease-linear duration-300"
            enterFrom="opacity-0"
            enterTo="opacity-100"
            leave="transition-opacity ease-linear duration-300"
            leaveFrom="opacity-100"
            leaveTo="opacity-0"
          >
            <div className="fixed inset-0 bg-black bg-opacity-25"/>
          </Transition.Child>

          <div className="fixed inset-0 z-40 flex">
            <Transition.Child
              as={Fragment}
              enter="transition ease-in-out duration-300 transform"
              enterFrom="translate-x-full"
              enterTo="translate-x-0"
              leave="transition ease-in-out duration-300 transform"
              leaveFrom="translate-x-0"
              leaveTo="translate-x-full"
            >
              <Dialog.Panel
                className="relative ml-auto flex h-full w-full max-w-xs flex-col overflow-y-auto bg-white py-4 pb-12 shadow-xl">
                <div className="flex items-center justify-between px-4">
                  <h2 className="text-lg font-medium text-gray-900">Filters</h2>
                  <button
                    type="button"
                    className="-mr-2 flex h-10 w-10 items-center justify-center rounded-md bg-white p-2 text-gray-400"
                    onClick={() => setMobileFiltersOpen(false)}
                  >
                    <span className="sr-only">Close menu</span>
                    <XMarkIcon className="h-6 w-6" aria-hidden="true"/>
                  </button>
                </div>

                {/* Filters */}
                <Form method="get" action="/" className="mt-4 border-t border-gray-200"
                      onSubmit={(event) => handleFilterSubmit(event)} reloadDocument>
                  <h3 className="sr-only">Categories</h3>
                  <ul role="list" className="px-2 py-3 font-medium text-gray-900">
                    {subCategories.map((category) => (<li key={category.name}>
                      <p className={classNames(category.current ? "underline" : '', "block px-2 py-3")}
                         onClick={
                           () => {
                             subCategories.forEach((c) => c.current = false);
                             category.current = true;
                             submitForm();
                           }
                         }>
                        {category.name}
                      </p>
                    </li>))}
                  </ul>

                  <Disclosure as="div" className="border-t border-gray-200 px-4 py-6">
                    {({open}) => (<>
                      <h3 className="-mx-2 -my-3 flow-root">
                        <Disclosure.Button
                          className="flex w-full items-center justify-between bg-white px-2 py-3 text-gray-400 hover:text-gray-500">
                          <span className="font-medium text-gray-900">Price range</span>
                          <span className="ml-6 flex items-center">
                                  {open ? (<MinusIcon className="h-5 w-5" aria-hidden="true"/>) : (
                                    <PlusIcon className="h-5 w-5" aria-hidden="true"/>)}
                                </span>
                        </Disclosure.Button>
                      </h3>
                      <Disclosure.Panel className="pt-6">
                        <PriceRange numbers={priceRange} onChange={handlePriceRangeChange}/>
                      </Disclosure.Panel>
                    </>)}
                  </Disclosure>

                  <div className="px-4">
                    <button
                      type="submit"
                      className="mt-8 w-full border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white bg-neutral-900 hover:bg-neutral-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-neutral-900transition-all"
                    >
                      Apply Filters
                    </button>
                  </div>
                </Form>
              </Dialog.Panel>
            </Transition.Child>
          </div>
        </Dialog>
      </Transition.Root>

      <main className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
        <div className="flex items-baseline justify-between border-b border-gray-200 pb-6 pt-12">
          <h1 className="text-2xl sm:text-4xl font-bold tracking-tight text-gray-900">Products</h1>

          <div className="flex items-center">
            <Menu as="div" className="relative inline-block text-left">
              <div>
                <Menu.Button
                  className="group inline-flex justify-center text-sm font-medium text-gray-700 hover:text-gray-900">
                  Sort
                  <ChevronDownIcon
                    className="-mr-1 ml-1 h-5 w-5 flex-shrink-0 text-gray-400 group-hover:text-gray-500"
                    aria-hidden="true"
                  />
                </Menu.Button>
              </div>

              <Transition
                as={Fragment}
                enter="transition ease-out duration-100"
                enterFrom="transform opacity-0 scale-95"
                enterTo="transform opacity-100 scale-100"
                leave="transition ease-in duration-75"
                leaveFrom="transform opacity-100 scale-100"
                leaveTo="transform opacity-0 scale-95"
              >
                <Menu.Items
                  className="absolute right-0 z-10 mt-2 w-40 origin-top-right rounded-md bg-white shadow-2xl ring-1 ring-black ring-opacity-5 focus:outline-none">
                  <div className="py-1">
                    {sortOptions.map((option) => (<Menu.Item key={option.name}>
                      {({active}) => (<a
                        href={option.href}
                        className={classNames(option.current ? 'font-medium text-gray-900' : 'text-gray-500', active ? 'bg-gray-100' : '', 'block px-4 py-2 text-sm')}
                      >
                        {option.name}
                      </a>)}
                    </Menu.Item>))}
                  </div>
                </Menu.Items>
              </Transition>
            </Menu>

            <button
              type="button"
              className="-m-2 ml-4 p-2 text-gray-400 hover:text-gray-500 sm:ml-6 lg:hidden"
              onClick={() => setMobileFiltersOpen(true)}
            >
              <span className="sr-only">Filters</span>
              <FunnelIcon className="h-5 w-5" aria-hidden="true"/>
            </button>
          </div>
        </div>

        <section aria-labelledby="products-heading" className="pb-24 pt-6">
          <h2 id="products-heading" className="sr-only">
            Products
          </h2>

          <div className="grid grid-cols-1 gap-x-8 gap-y-10 lg:grid-cols-4">
            {/* Filters */}
            <div className="lg:col-span-1">
              <Form method="get" action="/" className="hidden lg:block sticky top-24 "
                    onSubmit={(event) => handleFilterSubmit(event)}
                    reloadDocument>
                <h3 className="sr-only">Categories</h3>
                <ul role="list" className="space-y-4 border-b border-gray-200 pb-6 text-sm font-medium text-gray-900">
                  {subCategories.map((category) => (<li key={category.name}>
                    <p onClick={
                      () => {
                        subCategories.forEach((c) => c.current = false);
                        category.current = true;
                        submitForm();
                      }
                    }
                       className={classNames(category.current ? 'underline' : '', 'cursor-pointer')}
                    >{category.name}</p>
                  </li>))}
                </ul>

                <Disclosure as="div" className="border-b border-gray-200 py-6">
                  {({open}) => (<>
                    <h3 className="-my-3 flow-root">
                      <Disclosure.Button
                        className="flex w-full items-center justify-between bg-white py-3 text-sm text-gray-400 hover:text-gray-500">
                        <span className="font-medium text-gray-900">Price range</span>
                        <span className="ml-6 flex items-center">
                              {open ? (<MinusIcon className="h-5 w-5" aria-hidden="true"/>) : (
                                <PlusIcon className="h-5 w-5" aria-hidden="true"/>)}
                            </span>
                      </Disclosure.Button>
                    </h3>
                    <Disclosure.Panel className="pt-6">
                      <PriceRange numbers={priceRange} onChange={handlePriceRangeChange}/>
                    </Disclosure.Panel>
                  </>)}
                </Disclosure>

                <button
                  type="submit"
                  className="mt-8 w-full border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white bg-neutral-900 hover:bg-neutral-800 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-neutral-900 transition-all"
                >
                  Apply Filters
                </button>

              </Form>

            </div>

            <div className="lg:col-span-3">
              {children}
            </div>
          </div>
        </section>
      </main>
    </div>
  </div>)
}
